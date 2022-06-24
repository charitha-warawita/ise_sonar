<template >
    <div v-if="itemType === 'age'" class="container">
        <div class="row">
            <div class="col-md-12"><h5>Current Age range is set to {{ currAgeRange}}. Enter new age range</h5></div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Min Age</label>
                <input type="text" class="form-control" id="inputEmail4" v-model=minAge >
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Max Age</label>
                <input type="text" class="form-control" id="inputEmail4" v-model=maxAge>
            </div>
            <div class="col-md-12">
                <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useQualStore.SaveQualification(itemType, taId, qualificationId)">Save qualification</button>
            </div>
        </div>
    </div>
    
</template>
<script setup>
import {useQualificationStore} from '@/stores/qualificationStore'
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'
const props = defineProps([ 'itemType' , 'taId', 'qualificationId' ])

var useQualStore = useQualificationStore()

const { currAgeRange, minAge, maxAge } = storeToRefs(useQualStore)

/*onMounted(() => {
    // console.log('on mounted call');
    useQualStore.GetQualification(18, 60)
})*/
    
</script>
<style>
    
</style>