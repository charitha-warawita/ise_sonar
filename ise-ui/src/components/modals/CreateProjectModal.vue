<script setup lang="ts">
import { reactive, computed } from 'vue';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import { useToast } from 'primevue/usetoast';

import PrimaryButton from '../buttons/PrimaryButton.vue';

// https://vuejs.org/guide/components/events.html#usage-with-v-model
// Configure two-way data binding. We recieve a prop, 'visible'.
// Create a computed varible, visible. Which points to our prop.
// When the variable is updated, set will emit the update event.
// Vue will then update the parent component's prop value.
// We do this because we need to be able to set visible when we create a project.
const props = defineProps({ visible: Boolean });
const emits = defineEmits(['update:visible']);
const visible = computed({
    get: () => props.visible,
    set: (value) => emits('update:visible', value)
});

interface ProjectDetails {
    name: string | null,
    maconomy: string | null,
    owner: string | null
};
const details : ProjectDetails = reactive({
    name: null,
    maconomy: null,
    owner: null
});

const toast = useToast();
const CreateProject = () => {
    // API call to create modal.
    
    toast.add({
        severity: "success",
        summary: "New Project Created",
        detail: "Successfully created a new project.",
        life: 3000    
    });

    visible.value = false; 
}

</script>

<template>
    <Dialog modal ref="dialog" v-model:visible="visible">
        <form class="project-form">
            <div class="project-form-inputs">
                <span class="p-float-label">
                    <InputText id="project-name" type="text" v-model="details.name"></InputText>
                    <label for="project-name">Project Name</label>
                </span>
                <span class="p-float-label">
                    <InputText id="maconomy-number" type="number" v-model="details.maconomy"></InputText>
                    <label for="maconomy-number">Maconomy Number</label>
                </span>
                <span class="p-float-label">
                    <InputText id="project-owner" type="text" v-model="details.owner"></InputText>
                    <label for="project-owner">Owner</label>
                </span>
            </div>

            <PrimaryButton square label="Create Project" @click="CreateProject" />
        </form>
    </Dialog>
</template>

<style scoped>
    .project-form-inputs > * {
        margin: 25px 0;
    }

</style>