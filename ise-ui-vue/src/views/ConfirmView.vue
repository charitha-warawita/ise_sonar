<template>
    <PageTitle title="Confirm Project Details"></PageTitle>
    <div><h5>Project Details: </h5>
    <pre>{{project}}</pre>
    </div>

    <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="this.$router.push('/')">Submit</button>
    <button class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;" v-on:click="this.$router.push('/')">Cancel</button>
</template>
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import {useProjectStore} from '@/stores/projectStore'
import {storeToRefs} from 'pinia'

var useProjStore = useProjectStore()

const { project } = storeToRefs(useProjStore)
</script>
<style>
    
</style>