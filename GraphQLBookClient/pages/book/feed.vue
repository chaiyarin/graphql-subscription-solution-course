<template>
    <div class="container mt-4">
        <h1 class="text-center mb-4">Authors List (Live)</h1>
        <div v-if="loading" class="text-center">Waiting for new authors...</div>
        <div v-else-if="error" class="alert alert-danger">Error receiving data!</div>
        <ul v-else>
            <li v-for="author in authors" :key="author.firstname">
                👤 {{ author.firstname }} {{ author.lastname }}
            </li>
        </ul>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { ApolloClient, InMemoryCache, HttpLink, split } from '@apollo/client/core';
import { GraphQLWsLink } from '@apollo/client/link/subscriptions';
import { createClient } from 'graphql-ws';
import { getMainDefinition } from '@apollo/client/utilities';
import { provideApolloClient } from '@vue/apollo-composable';
import gql from 'graphql-tag';

// ✅ กำหนด API URL
const httpLink = new HttpLink({
    uri: 'http://localhost:5283/graphql',
});

// ✅ ใช้ WebSocket สำหรับ Subscription
const wsLink = new GraphQLWsLink(
    createClient({
        url: 'ws://localhost:5283/graphql',
    })
);

// ✅ แยก WebSocket และ HTTP ปกติ
const link = split(
    ({ query }) => {
        const definition = getMainDefinition(query);
        return (
            definition.kind === 'OperationDefinition' &&
            definition.operation === 'subscription'
        );
    },
    wsLink,
    httpLink
);

// ✅ สร้าง Apollo Client ใหม่ใน Component
const apolloClient = new ApolloClient({
    link,
    cache: new InMemoryCache(),
});

// ✅ ใช้ Apollo Client ใน Component โดยตรง
provideApolloClient(apolloClient);

// ✅ GraphQL Subscription Query
const AUTHOR_SUBSCRIPTION = gql`
    subscription {
      onAuthorCreated {
        firstname
        lastname
      }
    }
  `;

// ✅ ใช้ Ref เพื่อเก็บค่า Authors
const authors = ref([]);
const loading = ref(true);
const error = ref(null);

// ✅ Subscribe เมื่อ Component ถูก Mount
onMounted(() => {
    apolloClient.subscribe({
        query: AUTHOR_SUBSCRIPTION,
    }).subscribe({
        next({ data }) {
            if (data?.onAuthorCreated) {
                authors.value.push(data.onAuthorCreated);
            }
            loading.value = false;
        },
        error(err) {
            console.error("❌ Subscription Error:", err);
            error.value = err;
            loading.value = false;
        }
    });
});
</script>

<style scoped>
.container {
    max-width: 600px;
}
</style>