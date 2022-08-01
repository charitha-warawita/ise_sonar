<script setup lang="ts">import type { Project } from '@/types/Project';
import { type Ref, ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import PrimaryButton from '../components/buttons/PrimaryButton.vue';

var route = useRoute();

const project : Ref<Project | null>  = ref(null);

onMounted(async () => {
    let id = route.params.id as string;

    // TODO: Move to project service.
    project.value = {
        Id: Number.parseInt(id),
        Name: "Random Project",
        MaconomyNumber: "123",
        Owner: "Me",
        CreationDate: new Date(),
        LastActivity: new Date()
    }
})

</script>

<template>    
    <div class="project-container">
        <div class="project-overview shadowed">
            <h1>{{ project?.Name }}</h1>
            <!-- TODO: Show further details. -->
        </div>

        <div class="project-details">
            <div class="configuration-container">

                <div class="target-audience-empty-container shadowed">
                    <div class="centered">
                        <p style="font-weight: bold;">There are No Target Audiences</p>
                        <p>Please create at least on target group to create the project</p>

                        <div style="max-width: 250px; margin: 20px auto;">
                            <PrimaryButton square label="Create Target Audience" />
                        </div>
                    </div>
                </div>

            </div>

            <div class="pricing-container">
                <div class="estimated-price shadowed">
                    <div class="estimated-price-calculation">
                        <h3 style="font-weight: bold;">Estimated Price</h3>
                    </div>
                    <div class="create-project-container">
                        <PrimaryButton square disabled label="Create Project" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>

.project-container { 
    flex-direction: column;
}

.project-overview {
    flex-grow: 1;
    padding: 20px;
    margin-bottom: 50px;
}

.project-details {
    flex-grow: 1;
    display: flex;
    flex-direction: row;
}

.configuration-container {     
    flex-grow: 0.85;
    display: flex;
    flex-direction: column;
    margin-right: 10px;
}

.pricing-container {
    flex-grow: 0.15;
    margin-left: 10px;
}

.target-audience-empty-container {
    /* flex-grow: 0.2; */
    padding: 20px;
    text-align: center;
    /* //height: 35vh; */
}

.estimated-price {
    height: 40vh;
    padding: 20px;
    display: flex;
    flex-direction: column;
}

.estimated-price-calculation {
    flex-grow: 1;
}

@media screen and (min-width: 992px) {

    .configuration-container {     
        flex-grow: 0.65;
    }

    .pricing-container {
        flex-grow: 0.35;
    }

    .estimated-price {
        height: 20vh;
    }
}

@media screen and (min-width: 1200px) {
    .configuration-container {     
        flex-grow: 0.85;
    }

    .pricing-container {
        flex-grow: 0.15;
    }

    .estimated-price {
        height: 25vh;
    }
}


.shadowed { 
    box-shadow: 0px 2px 10px var(--shadow-color);
}

.centered { 
    position: relative;
    top: 50%;
    transform: translateY(-50%);
}

</style>