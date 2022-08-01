<template>
    <div class="projectSetting">
        Select the main categories your study is about in order to make feasibility estimation as accurate as possible.
    </div>
    <p v-if="loading">Loading categories.. </p>
    <p v-if="error"> {{ error.message }} </p>
    <div v-if="categories">
    <!--<button> {{ category.name }} </button>-->
    <button 
        v-for="category in categories" 
        :key="category.id" 
        @click="toggleCategory(category.id)" 
        type="button" 
        :id="'cat'+category.id" 
        class="btn btn-outline-success btn-light me-2 projSettingTogButton col-md-2"
        >{{ category.name }}</button>
    </div><br/>
    <div class='basicSetting'>
        <div class="row g-3">
            <div class="col-md-12">
                Complete this information to provide suppiers with basic information of your survey
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Name</label>
                <input type="text" class="form-control" id="inputEmail4" v-model="project.name">
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Maconomy Reference</label>
                <input type="text" class="form-control" id="inputEmail4" v-model="project.reference">
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">User</label>
                <input type="text" class="form-control" id="inputEmail4" v-model="project.user.name">
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">User Email</label>
                <input type="email" class="form-control" id="inputEmail4" v-model="project.user.email">
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Start Date</label>
                <input type="date" class="form-control" id="inputEmail4" v-model="project.startDate">
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Fielding Period</label>
                <input type="number" class="form-control" id="inputEmail4" v-model="project.fieldingPeriod">
            </div>
            <div class="col-md-12">
                <label for="inputEmail4" class="form-label">Testing URL</label>
                <input type="text" class="form-control" id="inputEmail4" v-model="project.testingUrl">
            </div>
            <div class="col-md-12">
                <label for="inputEmail4" class="form-label">Live URL</label>
                <input type="text" class="form-control" id="inputEmail4" v-model="project.liveUrl">
            </div>
        </div>
    </div>
    <!--<RouterLink @click="useProjStore.CreateProject(project)" class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" to="/confirm">Create Project</RouterLink>
    <RouterLink @click="useProjStore.CreateProject(project)" class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;" to="/confirm">Save as Draft</RouterLink>
    <RouterLink class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;" to="/">Cancel</RouterLink>-->
                            
</template>
<script setup>
import {useProjectSettingStore} from '@/stores/projectSettingStore'
import { useProjectStore } from "@/stores/projectStore"; 
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'

var useProjSettingStore = useProjectSettingStore()
var useProjStore = useProjectStore();
var project = useProjStore.project;
const { categories, loading, error } = storeToRefs(useProjSettingStore)

const toggleCategory = (id) => {
    // var project = useProjectStore().project;
    var catId = 'cat'+ id;
    var element = document.getElementById(catId);
    var tempClass = element.className;
    if(tempClass.includes('btn-light')) {
        project.categories.push(id);
        element.className = tempClass.replace('btn-light', 'searchButton');
    }
    else {
        var currIndex = project.categories.indexOf(id);
        if(currIndex !== -1) project.categories.splice(currIndex, 1);
        element.className = tempClass.replace('searchButton', 'btn-light');;
    }
};

onMounted(() => {
    // console.log('on mounted call');
    useProjSettingStore.$reset()
    useProjSettingStore.FetchCategories()
})
</script>
<style>
.projectSetting {
    margin: 5px 0 5px 0;
}

.projSettingTogButton {
    margin:2px 0 2px 0; 
    font-size:0.80em;
}
    
</style>