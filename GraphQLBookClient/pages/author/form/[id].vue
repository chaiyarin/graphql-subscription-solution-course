<script setup>
import { ref, onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import {
  useQuery,
  provideApolloClient,
  useMutation,
} from "@vue/apollo-composable";
import gql from "graphql-tag";
import { ApolloClient, InMemoryCache, HttpLink } from "@apollo/client/core";

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

// ✅ สร้าง Apollo Client
const apolloClient = new ApolloClient({
  link: new HttpLink({ uri: "http://localhost:5283/graphql" }), // เปลี่ยนเป็น API จริงของคุณ
  cache: new InMemoryCache(),
});

// ✅ ให้ Vue ใช้ Apollo Client
provideApolloClient(apolloClient);

// ✅ ดึงข้อมูลผู้เขียน
const { result, loading, error, refetch } = useQuery(GET_AUTHOR_DETAIL);

// ✅ Model ใหม่ สำหรับเก็บข้อมูลผู้เขียน
const authorModel = ref({
  authorId: null,
  nickname: "",
  firstname: "",
  lastname: "",
  birthdayDate: "",
  coverProfileImageUrl: "",
});

// ✅ Map ค่าจาก Query ไปยัง authorModel (เมื่อ result เปลี่ยน)
watchEffect(() => {
  console.log(result.value);
  
  if (result.value?.authorsQuery?.authorById) {
    const author = result.value.authorsQuery.authorById;
    authorModel.value = {
     authorId: author.authorId,
     nickname: author.nickname || "",
     firstname: author.firstname || "",
     lastname: author.lastname || "",
     birthdayDate: author.birthdayDate || "",
     coverProfileImageUrl: author.coverProfileImageUrl || "",
    };
  }
});

// ✅ GraphQL Mutation
const MUTATION_UPDATEAUTHOR = gql`
   mutation UpdateAuthor(
   $authorId : Int!,
   $nickname : String!,
   $firstname : String!,
   $lastname : String!,
   $birthdayDate : String!,
   $coverProfileImageUrl : String!,
  ) {
    authorMutation {
      updateAuthor(
          authorId : $authorId
          nickname : $nickname
          firstname : $firstname
          lastname : $lastname
          birthdayDate : $birthdayDate
          coverProfileImageUrl : $coverProfileImageUrl
      ) {
        authorId
        nickname
        firstname
        lastname
        birthdayDate
        coverProfileImageUrl
      }
    }
  }
`;

// ✅ ใช้ Mutation แต่ไม่กำหนด `variables` ล่วงหน้า

// ✅ Submit Form (แก้ให้ `variables` ส่งค่าล่าสุด)
const submitForm = async () => {
  try {
    console.log("📤 Preparing to Submit...");

    // ❗ ตรวจสอบค่า `authorModel.value`
    if (!authorModel.value.authorId || !authorModel.value.nickname) {
      console.error("❌ Invalid authorModel:", authorModel.value);
      alert("Some required fields are missing.");
      return;
    }

    // ✅ ป้องกันค่าที่เป็น `null` หรือ `undefined`
    const variables = {
      authorId: authorModel.value.authorId,
      nickname: authorModel.value.nickname || "", // ป้องกัน `null`
      firstname: authorModel.value.firstname || "",
      lastname: authorModel.value.lastname || "",
      birthdayDate: authorModel.value.birthdayDate || "",
      coverProfileImageUrl: authorModel.value.coverProfileImageUrl || "",
    };

    console.log("📌 Variables Before Mutation:", variables);
    const { mutate } = useMutation(MUTATION_UPDATEAUTHOR, { variables });

    mutate().then((res) => {
      console.log("✅ Author Updated Successfully");
      router.push("/author");
    });

  } catch (error) {
    console.error("❌ Error updating Author:", error);
  }
};

// ✅ ล้าง Cache และ Refetch Query ทุกครั้งที่หน้าโหลด
onMounted(() => {
  console.log("🚀 Clearing Apollo Cache and Refetching Data...");
  apolloClient.cache.reset();
  refetch({ authorId: authorId.value });
});
</script>

<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Edit Author</h1>

    <!-- แสดง Loading -->
    <div v-if="loading" class="text-center">Loading author details...</div>
    <div v-else-if="error" class="alert alert-danger">
      Error loading author details!
    </div>
    <!-- Form -->
    <form v-if="authorModel.authorId" @submit.prevent="submitForm">
      <div class="mb-3">
        <label for="nickname" class="form-label">Nick Name</label>
        <input
          type="text"
          class="form-control"
          id="nickname"
          v-model="authorModel.nickname"
          required
        />
      </div>

    

      <div class="mb-3">
        <label for="firstname" class="form-label">First Name</label>
        <input
          type="text"
          class="form-control"
          id="firstname"
          v-model="authorModel.firstname"
          required
        />
      </div>

      <div class="mb-3">
        <label for="lastname" class="form-label">LastName</label>
        <input
          type="text"
          class="form-control"
          id="lastname"
          v-model="authorModel.lastname"
          required
        />
      </div>

      <div class="mb-3">
        <label for="birthDate" class="form-label">Birth Date</label>
        <input
          type="text"
          class="form-control"
          id="birthDate"
          v-model="authorModel.birthdayDate"
          required
        />
      </div>

      <div class="mb-3">
        <label for="coverProfileImageUrl" class="form-label">Cover Profile Image URL</label>
        <input
          type="url"
          class="form-control"
          id="coverProfileImageUrl"
          v-model="authorModel.coverProfileImageUrl"
        />
      </div>

      <button type="submit" class="btn btn-primary">Submit</button>
      <button class="btn btn-secondary mx-2" @click="router.push('/author')">Cancel</button>

    </form>
  </div>
</template>

<style scoped>
.img-fluid {
  max-height: 300px;
  object-fit: cover;
}
</style>
