<template>
    <PageTitle title="Confirm Project Details"></PageTitle>
    <div><h5>Project Details: </h5>
    <pre>{{project}}</pre>
    </div>

    <button @click="createNewProject()" class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;">Submit</button>
    <button @click="discardNewProject()" class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;">Discard</button>

    <span v-if="useProjStore.saveProjectLoading">Submitting the project</span>
    <!--<div class="col-md-12"><RouterLink class="btn btn-outline-success searchButton me-2" style="width:100%; margin-bottom: 10px;" to="/">Submit</RouterLink></div>
    <div class="col-md-12"><RouterLink class="btn btn-outline-success btn-light me-2" style="width:100%; margin-bottom: 10px;" to="/">Cancel</RouterLink></div>-->
</template>
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import {useProjectStore} from '@/stores/projectStore'
import {storeToRefs} from 'pinia'
import { useRouter } from 'vue-router';

var useProjStore = useProjectStore()

const { project } = storeToRefs(useProjStore)
const router = useRouter();
async function createNewProject() {
    await useProjStore.CreateProject(useProjStore.project);
    if(typeof(useProjStore.project.id) === undefined || useProjStore.project.errors.length > 0) {
        var newLine = "\r\n"
        var alertMessage = 'Project Save as Draft is unsuccessful. Project ID returned is: ' + useProjStore.project.id + '.' + newLine;
        alertMessage += 'Error returned are: ' + newLine;
        for(var i =0; i < useProjStore.project.errors.length; i++)
            alertMessage += useProjStore.project.errors[i] + newLine;
        alert(alertMessage);
    }
    else {
        alert('project successfully created. New project ID is ' + useProjStore.project.id);
        useProjStore.$reset();
    }
    router.push('/');
}

function discardNewProject() {
    if(confirm("Are you sure you want to cancel? All your changes will be discarded?")) {
        useProjStore.$reset();
        router.push('/');
    }
}
</script>
<style>
    
</style>