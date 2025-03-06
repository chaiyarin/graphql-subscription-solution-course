<template>
  <div>
    <h1>Books</h1>
    <div v-if="loading">Loading...</div>
    <div v-if="error">Error: {{ error.message }}</div>
    <ul>
      <!-- ตรวจสอบว่า result.value.onBookCreated เป็นอาร์เรย์หรือไม่ และแสดงข้อมูล -->
      <li v-for="book in books" :key="bookId">{{ book.bookName }} </li>
    </ul>
  </div>
</template>

<script setup>
import { useQuery, provideApolloClient,useSubscription } from '@vue/apollo-composable';
import { ApolloClient, InMemoryCache, HttpLink ,split} from '@apollo/client/core';
import gql from 'graphql-tag';
import { WebSocketLink } from '@apollo/client/link/ws';
import { getMainDefinition } from '@apollo/client/utilities';


  // สร้าง HttpLink สำหรับ query และ mutation
  const httpLink = new HttpLink({
    uri: 'http://localhost:5283/graphql',  // เปลี่ยนเป็น URL ของ GraphQL API
  });

  // สร้าง WebSocketLink สำหรับ subscription
  const wsLink = new WebSocketLink({
    uri: 'ws://localhost:5283/graphql',  // เปลี่ยนเป็น WebSocket URL ของ API
    options: {
      reconnect: true, // รีเชื่อมต่ออัตโนมัติเมื่อการเชื่อมต่อล้มเหลว
    },
  });
  // ใช้ split เพื่อตรวจสอบประเภทของ operation (query, mutation, หรือ subscription)
  const link = split(
    ({ query }) => {
      const { kind, operation } = getMainDefinition(query);
      return kind === 'OperationDefinition' && operation === 'subscription'; // ถ้าเป็น subscription ใช้ WebSocketLink
    },
    wsLink,  // ถ้าเป็น subscription ใช้ WebSocketLink
    httpLink  // ถ้าเป็น query หรือ mutation ใช้ HttpLink
  );

  // สร้าง ApolloClient
  const apolloClient = new ApolloClient({
    link: link,
    cache: new InMemoryCache(), // ใช้ InMemoryCache สำหรับการเก็บข้อมูล
  });

// ใช้ provideApolloClient เพื่อกำหนด Context ของ Apollo
provideApolloClient(apolloClient);
const MESSAGE_ADDED_SUBSCRIPTION = gql`
  subscription {
  onBookCreated {
    authorId
    bookId
    bookName
    coverBookImageUrl
    createdDate
    description
    isDeleted
    price
    rating
    updatedDate
  }
}
`

const { result, loading, error } = useSubscription(MESSAGE_ADDED_SUBSCRIPTION)
console.log(result);
console.log(error);
// สร้าง ref สำหรับเก็บข้อมูล books
const books = ref([])

// ใช้ watchEffect เพื่อตรวจสอบผลลัพธ์จาก subscription
watchEffect(() => {
  // ตรวจสอบว่า result.value และ onBookCreated มีค่าและเป็นอาร์เรย์
  if (result.value && result.value.onBookCreated) {
    // เพิ่มข้อมูลหนังสือใหม่เข้าไปใน books
    books.value = [result.value.onBookCreated, ...books.value]
  }
})
</script>
