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
        class="btn btn-outline-success btn-light me-2 projSettingTogButton"
        >{{ category.name }}</button>
        </div>
</template>
<script setup>
import {useProjectSettingStore} from '@/stores/projectSettingStore'
import { useProjectStore } from "@/stores/projectStore"; 
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'

var useProjSettingStore = useProjectSettingStore()

const { categories, loading, error } = storeToRefs(useProjSettingStore)

const toggleCategory = (id) => {
    var project = useProjectStore().project;
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