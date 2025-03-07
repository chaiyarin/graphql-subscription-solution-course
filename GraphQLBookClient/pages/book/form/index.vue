<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Create Book</h1>

    <!-- Form to Add/Edit Book -->
    <form @submit.prevent="submitForm">
      <div class="mb-3">
        <label for="bookName" class="form-label">Book Name</label>
        <input
          type="text"
          class="form-control"
          id="bookName"
          v-model="book.bookName"
          required
        />
      </div>

      <div class="mb-3">
        <label for="authorId" class="form-label">Author ID</label>
        <input
          type="number"
          class="form-control"
          id="authorId"
          v-model="book.authorId"
          required
        />
      </div>

      <div class="mb-3">
        <label for="description" class="form-label">Description</label>
        <textarea
          class="form-control"
          id="description"
          v-model="book.description"
          required
        ></textarea>
      </div>

      <div class="mb-3">
        <label for="price" class="form-label">Price</label>
        <input
          type="number"
          class="form-control"
          id="price"
          v-model="book.price"
          required
        />
      </div>

      <div class="mb-3">
        <label for="rating" class="form-label">Rating</label>
        <input
          type="number"
          class="form-control"
          id="rating"
          v-model="book.rating"
          required
          min="1"
          max="5"
        />
      </div>

      <div class="mb-3">
        <label for="coverBookImageUrl" class="form-label">Cover Image URL</label>
        <input
          type="url"
          class="form-control"
          id="coverBookImageUrl"
          v-model="book.coverBookImageUrl"
        />
      </div>

      <button type="submit" class="btn btn-primary">Submit</button>
      <button class="btn btn-secondary mx-2" @click="router.push('/')">Cancel</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useMutation } from "@vue/apollo-composable";
import gql from "graphql-tag";



// Reactive book data
const book = ref({
  bookName: '',
  authorId: null,
  description: '',
  price: 0,
  rating: 1,
  coverBookImageUrl: 'https://m.media-amazon.com/images/I/91dwIJk-iuL._SY522_.jpg',
  isDeleted: false,
  createdDate: new Date(),
  updatedDate: new Date(),
});

const router = useRouter();

// GraphQL mutation
const MUTATION_CREATEBOOK = gql`
  mutation CreateBook(
    $bookName: String!,
    $authorId: Int!,
    $description: String!,
    $price: Decimal!,
    $rating: Int!,
    $coverImageUrl: String!
  ) {
    bookMutation {
    createBook(
      bookName: $bookName
      description: $description
      price: $price
      authorId: $authorId
      rating: $rating
      coverImageUrl: $coverImageUrl
    ) {
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
}
`;

// Submit form function
const submitForm = async () => {
  // Use conditional logic to pass only non-null values
  const variables = {
    bookName: book.value.bookName || null, // Use null if undefined or empty
    authorId: book.value.authorId || null,
    description: book.value.description || null,
    price: book.value.price || null,
    rating: book.value.rating || null,
    coverImageUrl: book.value.coverBookImageUrl || null,
  };

  try {
    const { mutate } = useMutation(MUTATION_CREATEBOOK, { variables });
    mutate().then(res => {
      console.log("Book created:", res);
      router.push('/'); 
      setTimeout(() => location.reload(), 100); // Force a reload if needed

    })
  } catch (error) {
    console.error("Error creating book:", error);
  }
};
</script>

<style scoped>
.container {
  max-width: 600px;
  margin: 0 auto;
}
</style>
