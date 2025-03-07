<template>
  <div class="container mt-4">
    <h1 class="text-center mb-4">Author</h1>
    <div class=" d-flex flex-column ">
      <div class="align-self-end p-4">
        <nuxt-link
                    :to="`/Author/form`"
                    class="btn btn-primary mx-2"
                  >
                    Create Author
        </nuxt-link>
      </div>
    </div>
    <div v-if="loading" class="text-center">Loading Authors...</div>
    <div v-else-if="error" class="alert alert-danger">Error loading Authors!</div>
    <div v-else>
      <div class="row">
        <div
          v-for="author in result.authorsQuery.authors"
          :key="author.authorId"
          class="col-md-3 mb-4"
        >
          <div class="card h-100">
            <img
              :src="author.coverProfileImageUrl"
              class="card-img-top"
              alt="Author Cover"
            />
            <div class="card-body">
              <h5 class="card-title">{{ author.nickname }}</h5>
              <p class="card-text">{{ author.firstName }}</p>
              <h5 class="card-title">{{ author.lastName }}</h5>
            </div>
            <div class="card-footer text-center">
              <nuxt-link
                :to="`/Author/${author.authorId}`"
                class="btn btn-primary mx-2"
              >
                View Details
              </nuxt-link>
              <nuxt-link
                :to="`/Author/form/${author.authorId}`"
                class="btn btn-primary mx-2"
              >
                Edit Author
              </nuxt-link>
              <button
                class="btn btn-danger"
                @click="deleteAuthorByAuthorId(author.authorId)"
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
  useMutation,
} from "@vue/apollo-composable";
import gql from "graphql-tag";



const GET_Authors = gql`
  query Author {
    authorsQuery {
      authors {
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

const { result, loading, error, refetch } = useQuery(GET_Authors);

const deleteAuthorByAuthorId = (authorId) => {
  const MUTATION_DELETEAUTHOR = gql`
    mutation Author {
        authorMutation {
            deleteAuthor(authorId: ${authorId}) {
                authorId
            }
        }
    }
    `;
  const { mutate } = useMutation(MUTATION_DELETEAUTHOR);

  mutate()
    .then((res) => {
      refetch().then(res => {

      });
    })
    .catch((err) => {
      console.error("Error deleting Author:", err);
    });

onMounted(() => {
  refetch();
});
};
</script>

<style scoped>
.card-img-top {
  height: 200px;
  object-fit: cover;
}
</style>
