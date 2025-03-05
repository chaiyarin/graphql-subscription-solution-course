<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useQuery, useApolloClient } from '@vue/apollo-composable';
import gql from 'graphql-tag';

// ✅ ดึง Route Parameter
const route = useRoute();
const router = useRouter();
const bookId = ref(parseInt(route.params.id, 10));

console.log("🔍 Debug: bookId =", bookId.value);

// ✅ GraphQL Query
const GET_BOOK_DETAIL = gql`
query GetBookDetail($bookId: Int!) { 
  booksQuery {
    bookById(bookId: $bookId) { 
      bookId
      bookName
      description
      price
      rating
      createdDate
      coverBookImageUrl
      author {
        nickname
        firstname
        lastname
        birthdayDate
        coverProfileImageUrl
      }
    }
  }
}
`;

// ✅ ใช้ Apollo Client เพื่อ Clear Cache ทุกครั้ง
const { resolveClient } = useApolloClient();
const apolloClient = resolveClient();

// ✅ สร้าง Query และใช้ 'network-only' Policy เพื่อให้ Apollo ไม่ใช้ Cache
const { result, loading, error, refetch } = useQuery(GET_BOOK_DETAIL, {
  variables: { bookId: bookId.value },
  fetchPolicy: 'network-only', // ✅ ดึงข้อมูลใหม่เสมอ
});

// ✅ ล้าง Cache และ Refetch Query ทุกครั้งที่หน้าโหลด
onMounted(() => {
  console.log("🚀 Clearing Apollo Cache and Refetching Data...");
  apolloClient.cache.reset(); // 🔥 ล้าง Cache ของ Apollo
  refetch({ bookId: bookId.value }); // ✅ ดึงข้อมูลใหม่เสมอ
});
</script>

<template>
  <div class="container mt-4">
    <button class="btn btn-secondary mb-3" @click="router.push('/')">← Back to Book List</button>

    <div v-if="loading" class="text-center">Loading book details...</div>
    <div v-else-if="error" class="alert alert-danger">Error loading book details!</div>
    <div v-else>
      <div class="card shadow-lg p-4">
        <div class="row g-4">
          <!-- Cover Image -->
          <div class="col-md-4 text-center">
            <img :src="result.booksQuery.bookById.coverBookImageUrl" class="img-fluid rounded shadow" alt="Book Cover">
          </div>

          <!-- Book Details -->
          <div class="col-md-8">
            <h2 class="fw-bold">{{ result.booksQuery.bookById.bookName }}</h2>
            <p class="text-muted">Published: {{ new Date(result.booksQuery.bookById.createdDate).toLocaleDateString() }}
            </p>
            <p class="text-primary fs-4">Price: ${{ result.booksQuery.bookById.price }}</p>
            <p class="fw-bold text-warning">⭐ Rating: {{ result.booksQuery.bookById.rating }}/5</p>
            <p class="mt-3">{{ result.booksQuery.bookById.description || 'No description available.' }}</p>
          </div>
        </div>
      </div>

      <!-- Author Section -->
      <div class="card mt-4 shadow p-4">
        <div class="row g-4 align-items-center">
          <!-- Author Image -->
          <div class="col-md-3 text-center">
            <img :src="result.booksQuery.bookById.author.coverProfileImageUrl" class="img-fluid rounded-circle shadow"
              alt="Author Profile" style="width: 120px; height: 120px;">
          </div>
          <!-- Author Info -->
          <div class="col-md-9">
            <h4 class="fw-bold">Author: {{ result.booksQuery.bookById.author.firstname }} {{
              result.booksQuery.bookById.author.lastname }} ({{ result.booksQuery.bookById.author.nickname }})</h4>
            <p>Born: {{ result.booksQuery.bookById.author.birthdayDate }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.img-fluid {
  max-height: 300px;
  object-fit: cover;
}
</style>