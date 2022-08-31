<template>
    <PageTitle title="Confirm Project Details"></PageTitle>
    <div>
    <div class="row">
        <div class="col-md-6">
            <h5>ISE spec - Request</h5><hr>
            <pre>{{project}}</pre>
        </div>
        <div class="col-md-6">
            <h5>Converted Cint Specs - Requests </h5><hr>
            <div v-for="request in cintRequests" :key=request.name>
                <label><b>Cint Conversion validation result: </b>{{ request.validationResult }}</label><hr>
                <label><b>Cint Request:</b> {{ request.request.name }}</label>
                <pre>{{ request }}</pre><hr>
            </div>
            <!--<pre>{{cintRequests}}</pre>-->
        </div>
    </div>
    
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
import {useUserStore} from '@/stores/userStore'
import { onMounted } from 'vue'

var useProjStore = useProjectStore()
var userStore = useUserStore()

const { project, cintRequests } = storeToRefs(useProjStore)
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
        router.push('/');
    }
}

function discardNewProject() {
    if(confirm("Are you sure you want to cancel? All your changes will be discarded?")) {
        useProjStore.$reset();
        router.push('/');
    }
}

onMounted(async () => {
    if(!userStore.isAuthenticated()) {
      userStore.shallowLogout();
      router.push('/');
    }
})
</script>
<style>
    
</style>