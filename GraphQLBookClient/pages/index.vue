<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Book Store</h1>
    <div class=" d-flex flex-column ">
      <div class="align-self-end p-4">
        <nuxt-link
                    :to="`/book/form`"
                    class="btn btn-primary mx-2"
                  >
                    Create Book
        </nuxt-link>
      </div>
    </div>
    <div v-if="loading" class="text-center">Loading books...</div>
    <div v-else-if="error" class="alert alert-danger">Error loading books!</div>
    <div v-else>
      <div class="row">
        <div
          v-for="book in result.booksQuery.books"
          :key="book.bookId"
          class="col-md-3 mb-4"
        >
          <div class="card h-100">
            <img
              :src="book.coverBookImageUrl"
              class="card-img-top"
              alt="Book Cover"
            />
            <div class="card-body">
              <h5 class="card-title">{{ book.bookName }}</h5>
              <p class="card-text">by {{ book.author.nickname }}</p>
              <p class="text-primary">Price: ${{ book.price }}</p>
            </div>
            <div class="card-footer text-center">
              <nuxt-link
                :to="`/book/${book.bookId}`"
                class="btn btn-primary mx-2"
              >
                View Details
              </nuxt-link>
              <button
                class="btn btn-danger"
                @click="deleteBookByBookId(book.bookId)"
              >
                Delete
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import {
  useQuery,
  provideApolloClient,
  useMutation,
} from "@vue/apollo-composable";
import { ApolloClient, InMemoryCache, HttpLink } from "@apollo/client/core";
import gql from "graphql-tag";

// สร้าง Apollo Client ใหม่ใน Component (ถ้า Plugin ไม่โหลด)
const apolloClient = new ApolloClient({
  link: new HttpLink({ uri: "http://localhost:5283/graphql" }), // เปลี่ยนเป็น API จริงของคุณ
  cache: new InMemoryCache(),
});

// ใช้ provideApolloClient เพื่อกำหนด Context ของ Apollo
provideApolloClient(apolloClient);

const GET_BOOKS = gql`
  query {
    booksQuery {
      books {
        bookId
        bookName
        author {
          nickname
        }
        coverBookImageUrl
        price
      }
    }
  }
`;

const { result, loading, error, refetch } = useQuery(GET_BOOKS);

const deleteBookByBookId = (bookId) => {
  const MUTATION_DELETEBOOK = gql`
    mutation Book {
        bookMutation {
            deleteBook(bookId: ${bookId}) {
                bookId
            }
        }
    }
    `;
  const { mutate } = useMutation(MUTATION_DELETEBOOK);

  mutate()
    .then((res) => {
      refetch();
    })
    .catch((err) => {
      console.error("Error deleting book:", err);
    });
};
</script>

<style scoped>
.card-img-top {
  height: 200px;
  object-fit: cover;
}
</style>
