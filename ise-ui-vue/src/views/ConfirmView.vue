<template>
    <PageTitle title="Confirm Project Details"></PageTitle>
    <div><h5>Project Details: </h5>
    <pre>{{project}}</pre>
    </div>

    <RouterLink class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" to="/">Submit</RouterLink>
    <RouterLink class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;" to="/">Cancel</RouterLink>

    <!--<div class="col-md-12"><RouterLink class="btn btn-outline-success searchButton me-2" style="width:100%; margin-bottom: 10px;" to="/">Submit</RouterLink></div>
    <div class="col-md-12"><RouterLink class="btn btn-outline-success btn-light me-2" style="width:100%; margin-bottom: 10px;" to="/">Cancel</RouterLink></div>-->
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