<script setup lang="ts">
import Panel from 'primevue/panel';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import ProjectsGrid from "../components/ProjectsGrid.vue";    
import { subDays, format } from 'date-fns';
import { ref, type Ref } from 'vue';
import PrimaryButton from '../components/buttons/PrimaryButton.vue';

const projectFilter: Ref<string|null> = ref(null);
const projects = ref([
    {
        Id: "12345",
        Name: "Something",
        CreationDate: format(subDays(new Date(), 25), "dd/MM/yyyy"),
        LastActivity: format(subDays(new Date(), 5), "dd/MM/yyyy - hh:mm aa"),    
    },
    {
        Id: "12345",
        Name: "Something",
        CreationDate: format(subDays(new Date(), 25), "dd/MM/yyyy"),
        LastActivity: format(subDays(new Date(), 5), "dd/MM/yyyy - hh:mm aa"),    
    },
    {
        Id: "12345",
        Name: "Something",
        CreationDate: format(subDays(new Date(), 25), "dd/MM/yyyy"),
        LastActivity: format(subDays(new Date(), 5), "dd/MM/yyyy - hh:mm aa"),    
    }
])

const filter = (e: Event) => {
    console.log(projectFilter.value);

    // API call to filter projects array.
}

</script>

<template>
    <div class="home-container" >
        <div class="home-top-row">
            <div style="flex-grow: 1;">
                <span class="p-float-label">
                    <InputText class="project-filter-input" id="search" type="text" v-model="projectFilter" @change="filter" />
                    <label class="project-filter-input-label" for="search">Search</label>
                </span>
                
            </div>
            <div style="flex-grow: 0.2; max-width: 250px;">
                <PrimaryButton square label="Create Project" />
            </div>
        </div>
        <div>
            <Panel :value="projects">
                <template #header>
                    <span>
                        <!-- Needs to be bold -->
                        <b>Projects <span style="color: lightskyblue;">{{ projects.length }}</span></b>
                    </span>
                </template>

                <ProjectsGrid :value="projects" />
            </Panel>            
        </div>
    </div>
</template>

<style scoped>

.home-container {
    display: flex;
    flex-direction: column;
}

.home-top-row {
    display: flex;
    flex-direction: row;
    margin-bottom: 30px;
}

/* Panel */
.p-panel :deep(.p-panel-header) {
    --border-radius: 10px;

    border: none;
    box-shadow: 0px 2px 10px var(--shadow-color);
    z-index: 1;
    background-color: var(--color-background);    
    border-top-left-radius: var(--border-radius);
    border-top-right-radius: var(--border-radius);
}

.p-panel :deep(.p-panel-content) {
    --border-radius: 10px;

    border: none;
    box-shadow: 0px 5px 12px var(--shadow-color);     
    border-bottom-left-radius: var(--border-radius);
    border-bottom-right-radius: var(--border-radius); 
}

/* Filter Input */
@media screen and (max-width: 750px) {
    .project-filter-input {
        width: 90%;
    }
}

@media screen and (min-width: 750px) {
    .project-filter-input {
        max-width: 535px;
        width: 65%;
    }
}

.project-filter-input {
    background: var(--blue-100);
    border: none;
}

.project-filter-input-label {
    color: var(--blue-500);
}

</style>