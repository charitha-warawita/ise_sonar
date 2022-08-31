
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import ProjectList from '@/components/ProjectList.vue'
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

<template>

  <PageTitle title="Projects"></PageTitle>
  <ProjectList/><br/>
  <main>
    <!-- <TheWelcome /> -->
    
  </main>
</template>
