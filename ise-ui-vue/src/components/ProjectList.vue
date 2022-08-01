<template>
    <div>
        <button class="btn btn-outline-success searchButton mb-4 " @click="toggleModal(999);">Create Project</button>
        <CustomModal @close="toggleModal(999)" modalId='999'>
            <div class="card modal-content">
                <h3 class="card-header">Create Project</h3>
                <div class="card-body">
                    <ProjectSetting/>
                </div>
            </div>
        </CustomModal>
    </div><hr/>
    <div>
        <ul class="nav nav-pills nav-fill border-bottom">
            <li class="nav-item">
            <a v-on:click="useProjStore.getProjectsBySearchNameAndStartDate('All')" :class="[currentStatus === 'All' ? 'active' : '']" class="nav-link" aria-current="page" href="#">All</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjStore.getProjectsBySearchNameAndStartDate('Active')" :class="[currentStatus === 'Active' ? 'active' : '']" class="nav-link" href="#">Active</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjStore.getProjectsBySearchNameAndStartDate('Draft')" :class="[currentStatus === 'Draft' ? 'active' : '']" class="nav-link" href="#">Draft</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjStore.getProjectsBySearchNameAndStartDate('Closed')" :class="[currentStatus === 'Closed' ? 'active' : '']" class="nav-link" href="#">Closed</a>
            </li>
        </ul><br /><br/>
        <div class="d-flex">
            <input class="form-control me-2" type="name" placeholder="Type name here" v-model="searchByName">
            <input class="form-control me-2" type="date" placeholder="Start date  ." aria-label="Date" v-model="searchByStartDate">
            <a class="btn btn-outline-success searchButton me-2 " v-on:click="useProjStore.getProjectsBySearchNameAndStartDate(useProjStore.currentStatus)">Filter</a>
            <a class="btn btn-outline-success searchButton me-2" v-on:click="useProjStore.setDefaultProjectList">Clear</a>
        </div>
        <br/><br/>
        <ProjectListTable class="projListTable" :fields='displayfields' :projects='useProjStore.currentProjects' :fieldTitles='displayFieldFormatted'></ProjectListTable>
    </div>
</template>
<script setup>
import {useProjectsStore} from '@/stores/projectsStore'
import {storeToRefs} from 'pinia'
import ProjectListTable from '@/components/ProjectListTable.vue'
import ProjectModel from '@/models/projectModel.js'
import {ref} from "vue"
import { onMounted } from 'vue'

import CustomModal from '@/components/CustomModal.vue'
import ProjectSetting from '@/components/ProjectSetting.vue'

// console.log(storeToRefs(useProjectStore()).projects);
var useProjStore = useProjectsStore()
const { projects, searchByName, searchByStartDate, currentProjects, currentStatus, getDraftProjects } = storeToRefs(useProjStore)
const displayfields = ProjectModel.displayfields
const displayFieldFormatted = ProjectModel.displayFieldFormatted

const toggleModal = (id) => {
    var classname = 'customModal-' + id
    var element = document.getElementsByClassName(classname)
    if(element[0].style.display === 'none') {
        element[0].style.removeProperty("display")
        element[1].style.removeProperty("display")
    }
    else { 
        element[0].style.display = 'none'
        element[1].style.display = 'none'
    }
};

onMounted(() => {
    // console.log('on mounted call');
    useProjStore.setDefaultProjectList()
})
</script>
<style>
input[type="date"]:not(.has-value):before{
  color: gray;
  content: attr(placeholder);
}

.projListTable {
    box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
}
</style>