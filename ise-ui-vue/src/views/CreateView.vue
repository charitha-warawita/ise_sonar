<template>
    <PageTitle title="Create new project"></PageTitle>
    <CreateProject />  
</template>
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import CreateProject from '@/components/CreateProject.vue'
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
</style>