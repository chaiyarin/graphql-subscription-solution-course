<script setup>
import { useSubscription } from '@vue/apollo-composable';
import { gql } from '@apollo/client/core';

const ON_AUTHOR_CREATED = gql`
  subscription OnAuthorCreated {
    onAuthorCreated {
      firstname
      lastname
    }
  }
`;

const { result } = useSubscription(ON_AUTHOR_CREATED);

// ตรวจสอบค่า result
watch(result, () => {
    console.log("🔍 Subscription Data:", result.value);
});
</script>

<template>
    <div>
        <h1>📡 Author Feed (Live Updates)</h1>
        <p v-if="loading">Waiting for new authors...</p>
        <p v-if="error" class="error">Error: {{ error.message }}</p>
        <ul v-if="result?.onAuthorCreated">
            <li>
                👤 {{ result.onAuthorCreated.firstname }} {{ result.onAuthorCreated.lastname }}
            </li>
        </ul>
    </div>
</template>

<style scoped>
.error {
    color: red;
    font-weight: bold;
}
</style>