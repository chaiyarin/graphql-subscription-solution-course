<script setup>
import { useSubscription } from '@vue/apollo-composable';
import { gql } from '@apollo/client/core';
import { ref, watch, onMounted } from 'vue';

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

// ฟังก์ชันสำหรับขออนุญาตใช้ Notification API
const requestNotificationPermission = () => {
    if ("Notification" in window) {
        if (Notification.permission !== "granted") {
            Notification.requestPermission().then(permission => {
                console.log("Notification permission:", permission);
            });
        }
    }
};

// ฟังก์ชันสำหรับแสดงการแจ้งเตือน
const showNotification = (author) => {
    if ("Notification" in window && Notification.permission === "granted") {
        new Notification("📢 New Author Created", {
            body: `${author.firstname} ${author.lastname} has joined!`,
            icon: author.coverProfileImageUrl || "https://via.placeholder.com/100"
        });
    }
};

// ตรวจสอบค่า result
watch(result, () => {
    console.log(result);
    if (result.value) {
        const newAuthor = {
            firstname: result.value.onAuthorCreated.firstname,
            lastname: result.value.onAuthorCreated.lastname,
            coverProfileImageUrl: result.value.onAuthorCreated.coverProfileImageUrl
        };
        authors.value.push(newAuthor);
        showNotification(newAuthor);
    }
    console.log("📌 อัปเดตรายชื่อ:", authors.value);
});

// ขออนุญาต Notification เมื่อ Component ถูกโหลด
onMounted(() => {
    requestNotificationPermission();
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