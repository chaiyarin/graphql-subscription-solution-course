<script setup>
import { ref, onMounted, watchEffect } from "vue";
import { useRoute, useRouter } from "vue-router";
import {
  useQuery,
  useMutation,
} from "@vue/apollo-composable";
import gql from "graphql-tag";

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
          authorId
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

// ✅ ดึงข้อมูลหนังสือ
const { result, loading, error, refetch } = useQuery(GET_BOOK_DETAIL, {
  variables: { bookId: bookId.value },
  fetchPolicy: "network-only",
});

// ✅ Model ใหม่ สำหรับเก็บข้อมูลหนังสือ
const bookModel = ref({
  bookId: null,
  bookName: "",
  description: "",
  price: null,
  rating: null,
  coverImageUrl: "",
  authorId: null,
});

// ✅ Map ค่าจาก Query ไปยัง bookModel (เมื่อ result เปลี่ยน)
watchEffect(() => {
  if (result.value?.booksQuery?.bookById) {
    const book = result.value.booksQuery.bookById;
    bookModel.value = {
      bookId: book.bookId,
      bookName: book.bookName || "",
      description: book.description || "",
      price: book.price || null,
      rating: book.rating || null,
      coverImageUrl: book.coverBookImageUrl || "",
      authorId: book.author?.authorId || null,
    };
  }
});

// ✅ GraphQL Mutation
const MUTATION_UPDATEBOOK = gql`
   mutation UpdateBook(
    $bookId: Int!,
    $bookName: String!,
    $authorId: Int!,
    $description: String!,
    $price: Decimal!,
    $rating: Int!,
    $coverImageUrl: String!
  ) {
    bookMutation {
      updateBook(
        bookId: $bookId
        bookName: $bookName
        description: $description
        price: $price
        authorId: $authorId
        rating : $rating
        coverImageUrl: $coverImageUrl
      ) {
        bookId
        bookName
        price
        rating
        description
        coverBookImageUrl
        updatedDate
      }
    }
  }
`;

// ✅ ใช้ Mutation แต่ไม่กำหนด `variables` ล่วงหน้า

// ✅ Submit Form (แก้ให้ `variables` ส่งค่าล่าสุด)
const submitForm = async () => {
  try {
    console.log("📤 Preparing to Submit...");

    // ❗ ตรวจสอบค่า `bookModel.value`
    if (!bookModel.value.bookId || !bookModel.value.bookName) {
      console.error("❌ Invalid bookModel:", bookModel.value);
      alert("Some required fields are missing.");
      return;
    }

    // ✅ ป้องกันค่าที่เป็น `null` หรือ `undefined`
    const variables = {
      bookId: bookModel.value.bookId,
      bookName: bookModel.value.bookName || "",
      authorId: bookModel.value.authorId || 0, // ป้องกัน `null`
      description: bookModel.value.description || "",
      price: bookModel.value.price || 0, // GraphQL อาจไม่รองรับ `null`
      rating: bookModel.value.rating || 0,
      coverImageUrl: bookModel.value.coverImageUrl || "",
    };

    console.log("📌 Variables Before Mutation:", variables);
    const { mutate } = useMutation(MUTATION_UPDATEBOOK, { variables });

    mutate().then((res) => {
      console.log("✅ Book Updated Successfully");
      router.push("/");
      setTimeout(() => location.reload(), 100); // Force a reload if needed

    });

  } catch (error) {
    console.error("❌ Error updating book:", error);
  }
};

// ✅ ล้าง Cache และ Refetch Query ทุกครั้งที่หน้าโหลด
onMounted(() => {
  console.log("🚀 Clearing Apollo Cache and Refetching Data...");
  refetch({ bookId: bookId.value });
});
</script>

<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Edit Book</h1>

    <!-- แสดง Loading -->
    <div v-if="loading" class="text-center">Loading book details...</div>
    <div v-else-if="error" class="alert alert-danger">
      Error loading book details!
    </div>

    <!-- Form -->
    <form v-if="bookModel.bookId" @submit.prevent="submitForm">
      <div class="mb-3">
        <label for="bookName" class="form-label">Book Name</label>
        <input
          type="text"
          class="form-control"
          id="bookName"
          v-model="bookModel.bookName"
          required
        />
      </div>

      <div class="mb-3">
        <label for="authorId" class="form-label">Author ID</label>
        <input
          type="number"
          class="form-control"
          id="authorId"
          v-model="bookModel.authorId"
          required
        />
      </div>

      <div class="mb-3">
        <label for="description" class="form-label">Description</label>
        <textarea
          class="form-control"
          id="description"
          v-model="bookModel.description"
          required
        ></textarea>
      </div>

      <div class="mb-3">
        <label for="price" class="form-label">Price</label>
        <input
          type="number"
          class="form-control"
          id="price"
          v-model="bookModel.price"
          required
        />
      </div>

      <div class="mb-3">
        <label for="rating" class="form-label">Rating</label>
        <input
          type="number"
          class="form-control"
          id="rating"
          v-model="bookModel.rating"
          required
          min="1"
          max="5"
        />
      </div>

      <div class="mb-3">
        <label for="coverImageUrl" class="form-label">Cover Image URL</label>
        <input
          type="url"
          class="form-control"
          id="coverImageUrl"
          v-model="bookModel.coverImageUrl"
        />
      </div>

      <button type="submit" class="btn btn-primary">Submit</button>
      <button class="btn btn-secondary mx-2" @click="router.push('/')">Cancel</button>

    </form>
  </div>
</template>

<style scoped>
.img-fluid {
  max-height: 300px;
  object-fit: cover;
}
</style>
