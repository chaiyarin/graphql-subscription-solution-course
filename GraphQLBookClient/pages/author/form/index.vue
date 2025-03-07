
<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Create Author</h1>

    <!-- แสดง Loading -->
    <div v-if="loading" class="text-center">Loading author details...</div>
    <div v-else-if="error" class="alert alert-danger">
      Error loading author details!
    </div>
    <!-- Form -->
    <form @submit.prevent="submitForm">
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


<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useMutation } from "@vue/apollo-composable";
import gql from "graphql-tag";


// Reactive Author data
const authorModel = ref({
  authorId: null,
  nickname: "",
  firstname: "",
  lastname: "",
  birthdayDate: "",
  coverProfileImageUrl: "https://m.media-amazon.com/images/I/61ImI9wz84L._SY522_.jpg",
});


const router = useRouter();

// GraphQL mutation
const MUTATION_CREATEAUTHOR = gql`
  mutation CreateAuthor(
   $nickname : String!,
   $firstname : String!,
   $lastname : String!,
   $birthdayDate : String!,
   $coverProfileImageUrl : String!,
  ) {
    authorMutation {
    createAuthor(
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

// Submit form function
const submitForm = async () => {
  // Use conditional logic to pass only non-null values
  const variables = {
      nickname: authorModel.value.nickname || "", // ป้องกัน `null`
      firstname: authorModel.value.firstname || "",
      lastname: authorModel.value.lastname || "",
      birthdayDate: authorModel.value.birthdayDate || "",
      coverProfileImageUrl: authorModel.value.coverProfileImageUrl || "",
  };


  try {
    const { mutate } = useMutation(MUTATION_CREATEAUTHOR, { variables });
    mutate().then(res => {
      console.log("Author created:", res);
      router.push('/author'); 
      setTimeout(() => location.reload(), 100); // Force a reload if needed
    })

  } catch (error) {
    console.error("Error creating Author:", error);
  }
};
</script>

<style scoped>
.container {
  max-width: 600px;
  margin: 0 auto;
}
</style>
