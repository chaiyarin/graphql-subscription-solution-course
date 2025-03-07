<script setup>
import { ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useQuery } from '@vue/apollo-composable';
import gql from 'graphql-tag';

// ✅ ดึง Route Parameter
const route = useRoute();
const router = useRouter();
const authorId = ref(parseInt(route.params.id, 10));

console.log("🔍 Debug: authorId =", authorId.value);

// ✅ GraphQL Query
const GET_AUTHOR_DETAIL = gql`
query Author {
  authorsQuery {
    authorById(authorId: ${authorId.value}) {
      authorId
      birthdayDate
      coverProfileImageUrl
      createdDate
      firstname
      isDeleted
      lastname
      nickname
      updatedDate
    }
  }
}

 
`;



// ✅ สร้าง Query และใช้ 'network-only' Policy เพื่อให้ Apollo ไม่ใช้ Cache
const { result, loading, error, refetch } = useQuery(GET_AUTHOR_DETAIL);

// ✅ ล้าง Cache และ Refetch Query ทุกครั้งที่หน้าโหลด
onMounted(() => {
  console.log("🚀 Clearing Apollo Cache and Refetching Data...");
  refetch(); // ✅ ดึงข้อมูลใหม่เสมอ
});
</script>

<template>
  <div class="container mt-4">
    <button class="btn btn-secondary mb-3" @click="router.push('/author')">← Back to Author List</button>
      <!-- Author Section -->
      <div class="card mt-4 shadow p-4">
        <div class="row g-4 align-items-center">
          <!-- Author Image -->
          <div class="col-md-3 text-center">
            <img :src="result?.authorsQuery?.authorById.coverProfileImageUrl" class="img-fluid rounded-circle shadow"
              alt="Author Profile" style="width: 120px; height: 120px;">
          </div>
          <!-- Author Info -->
          <div class="col-md-9">
            <h4 class="fw-bold">Author: {{ result?.authorsQuery?.authorById.firstname }} {{
              result?.authorsQuery?.authorById.lastname }} ({{ result?.authorsQuery?.authorById.nickname }})</h4>
            <p>Born: {{ result?.authorsQuery?.authorById.birthdayDate }}</p>
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