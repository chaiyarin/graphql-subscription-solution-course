<script setup>
import { useSubscription } from '@vue/apollo-composable';
import { gql } from '@apollo/client/core';

const ON_AUTHOR_CREATED = gql`
  subscription OnAuthorCreated {
    onAuthorCreated {
      firstname
      lastname
      coverProfileImageUrl
    }
  }
`;

const { result } = useSubscription(ON_AUTHOR_CREATED);
const authors = ref([]);

// ตรวจสอบค่า result
watch(result, () => {
    console.log(result);
    if (result.value) {
        authors.value.push({
            firstname: result.value.onAuthorCreated.firstname,
            lastname: result.value.onAuthorCreated.lastname,
            coverProfileImageUrl: result.value.onAuthorCreated.coverProfileImageUrl
        });
    }
    console.log("📌 อัปเดตรายชื่อ:", authors.value);
});
</script>

<template>
    <div class="container">
        <h1 class="my-4">📡 Author Feed (Live Updates)</h1>
        <ul v-if="authors.length > 0" class="list-group">
            <li v-for="(author, index) in authors" :key="index" class="list-group-item d-flex align-items-center">
                <img :src="author.coverProfileImageUrl" alt="Profile Image" class="rounded-circle me-3" width="50"
                    height="50">
                <div>
                    <strong>{{ author.firstname }}</strong> {{ author.lastname }}
                </div>
            </li>
        </ul>
        <p v-else class="text-muted">ยังไม่มีข้อมูล</p>
    </div>
</template>

<style scoped>
.error {
    color: red;
    font-weight: bold;
}
</style>