<template>
    <div>
        <ul class="nav nav-pills nav-fill border-bottom">
            <li class="nav-item">
            <a v-on:click="activetab='All'; filterStatus('All')" :class="[activetab === 'All' ? 'active' : '']" class="nav-link" aria-current="page" href="#">All</a>
            </li>
            <li class="nav-item">
            <a v-on:click="activetab='Active'; filterStatus('Active')" :class="[activetab === 'Active' ? 'active' : '']" class="nav-link" href="#">Active</a>
            </li>
            <li class="nav-item">
            <a v-on:click="activetab='Draft'; filterStatus('Draft')" :class="[activetab === 'Draft' ? 'active' : '']" class="nav-link" href="#">Draft</a>
            </li>
            <li class="nav-item">
            <a v-on:click="activetab='Closed'; filterStatus('Closed')" :class="[activetab === 'Closed' ? 'active' : '']" class="nav-link" href="#">Closed</a>
            </li>
        </ul><br /><br/>
        <div class="d-flex">
            <input class="form-control me-2" type="name" placeholder="Type name here" v-model="searchName">
            <input class="form-control me-2" type="date" placeholder="Start date  ." aria-label="Date" v-model="searchStartDate">
            <button class="btn btn-outline-success searchButton me-2 " @click="filterByNameStartDate">Filter</button>
            <button class="btn btn-outline-success searchButton me-2" @click="clearFilters">Clear</button>
        </div>
        <br/><br/>

        <ProjectListTable class="projListTable" :fields='displayfields' :projects='currentProject' :fieldTitles='displayFieldFormatted'></ProjectListTable>
        
 
    </div>
</template>
<script setup>
import {useProjectStore} from '@/stores/projectStore'
import {storeToRefs} from 'pinia'
import ProjectListTable from '@/components/ProjectListTable.vue'
import ProjectModel from '@/models/projectModel.js'

// console.log(storeToRefs(useProjectStore()).projects);
var useProjStore = useProjectStore()
const { projects, getDraftProjects, getActiveProjects, getClosedProjects } = storeToRefs(useProjStore)
const displayfields = ProjectModel.displayfields
const displayFieldFormatted = ProjectModel.displayFieldFormatted
</script>
<script>
export default {
    name: "projectList",
    props: {
        activetab: {
            type: String,
            default: 'All'
        }
    },
    components: { ProjectListTable },
    data() {
        return {
            currentProject: this.projects,
            searchName: '',
            searchStartDate: ''

        }
    },
    methods: {
        filterStatus(status) {
            // let test = this.useProjStore.getProjectByStatus(status);
            this.currentProject = this.useProjStore.getProjectByStatus(status);
        },
        filterByNameStartDate()
        {
            this.currentProject = this.useProjStore.getProjectByNameStartDate(this.searchName, this.searchStartDate);
        },
        clearFilters()
        {
            this.searchName = ''; this.searchStartDate = '';
            this.currentProject = this.useProjStore.getProjectByNameStartDate('','');
        }
    },
}
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