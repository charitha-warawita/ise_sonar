<template>
    <div>
      <div class="container is-fluid" style="margin: 20px 0 20px 0">
        <div v-if="error" class="notification is-danger is-4 title">
          {{ error }}
        </div>
  
        <Login @loginComplete="updateUser()" />
      </div>
    </div>
  </template>
  <script setup>
    import {useUserStore} from '@/stores/userStore'
    import {storeToRefs} from 'pinia'
    import Login from '@/components/Login.vue'
    import { useRouter } from 'vue-router';
    
    const router = useRouter();
    var userStore = useUserStore()
    const { user, error } = storeToRefs(userStore)
    
    function updateUser() {
      userStore.updateUser();
      userStore.fetchGraphDetails();
      router.push('/');
    }
    
</script>
<style></style>