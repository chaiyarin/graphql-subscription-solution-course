<script setup>
import { ref, computed, watch } from 'vue';
import { useRoute } from 'vue-router';
import { useSubscription, useMutation } from '@vue/apollo-composable';
import { gql } from '@apollo/client/core';

// ดึง roomId จาก URL
const route = useRoute();
const roomId = computed(() => route.params.roomId);

// 🟢 GraphQL Subscription - รับข้อความแบบ Real-time
const SUBSCRIBE_TO_MESSAGES = gql`
  subscription subscribeToMessages($receiverUsername: String!) {
    subscribeToMessages(receiverUsername: $receiverUsername) {
      senderUsername
      message
    }
  }
`;

// 🟢 GraphQL Mutation - ส่งข้อความ
const SEND_MESSAGE = gql`
  mutation sendMessage($message: ChatMessageInput!) {
    chatMutation {
      sendMessage(message: $message) {
        senderUsername
        receiverUsername
        message
      }
    }
  }
`;

// เก็บข้อความทั้งหมดในห้อง
const messages = ref([]);

// ใช้ Subscription เพื่อติดตามข้อความที่ถูกส่งมาที่ห้องนี้
const { result } = useSubscription(SUBSCRIBE_TO_MESSAGES, {
    variables: { receiverUsername: "chaiyarin_room" }
});

const variable = {
    receiverUsername: roomId.value
};

// อัปเดตรายการข้อความแบบ Real-time
watch(result, () => {
    console.log('SEX');
    console.log(result);
    if (result.value?.subscribeToMessages) {
        messages.value.push(result.value.subscribeToMessages);
    }
});

// ใช้ Mutation สำหรับส่งข้อความ

// ตัวแปรเก็บข้อความใหม่ที่ผู้ใช้จะพิมพ์
const newMessage = ref("");

// ฟังก์ชันส่งข้อความ
const sendMessage = async () => {
    if (!newMessage.value) return;

    const variables = {
        message: {
            message: newMessage.value,
            receiverUsername: roomId.value,
            senderUsername: "aof" // เปลี่ยนเป็นผู้ใช้ที่ login จริง ๆ ในแอปของคุณ
        }
    }
    console.log(variables);
    const { mutate } = useMutation(SEND_MESSAGE, { variables });
    await mutate().then(() => {
        console.log("✉️ Message sent:", newMessage.value);
        console.log("✉️ RoomId sent:", roomId.value);
        newMessage.value = ""; // เคลียร์ input หลังจากส่งข้อความ
    });

};
</script>

<template>
    <div>
        <h1>💬 Chat Room: {{ roomId }}</h1>

        <div class="chat-box">
            <ul>
                <li v-for="(msg, index) in messages" :key="index">
                    <strong>{{ msg.senderUsername }}:</strong> {{ msg.message }}
                </li>
            </ul>
        </div>

        <div class="chat-input">
            <input v-model="newMessage" placeholder="Type a message..." @keyup.enter="sendMessage" />
            <button @click="sendMessage">Send</button>
        </div>
    </div>
</template>

<style scoped>
.chat-box {
    height: 300px;
    overflow-y: auto;
    border: 1px solid #ccc;
    padding: 10px;
    margin-bottom: 10px;
}

.chat-input {
    display: flex;
}

.chat-input input {
    flex: 1;
    padding: 8px;
}

.chat-input button {
    padding: 8px 15px;
    margin-left: 10px;
}
</style>