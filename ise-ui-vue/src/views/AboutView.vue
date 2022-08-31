<template>
  <div class="about">
    <h1>This is an page is under implementation</h1>
  </div>
</template>
<script setup>
  import {useUserStore} from '@/stores/userStore'
  import { useRouter } from 'vue-router';
  import { onMounted } from 'vue'
  
  var userStore = useUserStore()
  const router = useRouter();
  
  onMounted(async () => {
      if(!userStore.isAuthenticated()) {
        userStore.shallowLogout();
        router.push('/');
      }
  })
  </script>
<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
