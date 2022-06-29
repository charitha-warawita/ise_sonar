<template lang="">
    <div class="container">
        <div class="row">
            <div class="col-8">
                <div class="accordion" id="accordionPanelsStayOpenExample">
                    <div class="accordion-item customItem">
                        <h2 class="accordion-header" id="panelsStayOpen-headingOne">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                            <b>Basic Settings</b>
                        </button>
                        </h2>
                        <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingOne">
                            <div class="accordion-body">
                                <div class='basicSetting'>
                                    <div class="row g-3">
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-if="project.projectTargetAudiences" v-for="ta in project.projectTargetAudiences" :key="ta.id">
                        <div class="accordion-item customItem">
                            <h2 class="accordion-header" id="panelsStayOpen-headingTwo">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" :data-bs-target="'#panelsStayOpen-collapseTwo-' + ta.id" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                <b>Target Audience - {{ta.id}}</b>
                            </button>
                            </h2>
                            <div :id="'panelsStayOpen-collapseTwo-' + ta.id" class="accordion-collapse collapse show" aria-labelledby="panelsStayOpen-headingTwo">
                                <div class="accordion-body">
                                    <div class="row g-3">
                                        <div class="col-md-12">
                                            <label for="inputEmail4" class="form-label">Name</label>
                                            <input type="text" class="form-control" id="inputEmail4" v-model="ta.name">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Target Audience Order</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.audienceNumber">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Estimated IR (in %)</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.estimatedIR">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Estimated LOI (in mins)</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.estimatedLOI">
                                        </div>
                                        <div class="col-md-6">
                                            <label for="inputEmail4" class="form-label">Wanted Completes Count</label>
                                            <input type="number" class="form-control" id="inputEmail4" v-model="ta.wantedCompletes">
                                        </div>
                                        <div class="accordion-item-custom" v-if="ta.qualifications.length > 0">
                                            <h2 class="accordion-header" id="panelsStayOpen-headingThree">
                                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" :data-bs-target="'#panelsStayOpen-collapseThree-' + ta.id" aria-expanded="false" aria-controls="panelsStayOpen-collapseTwo">
                                                    <b>Qualification</b>
                                                </button>
                                            </h2>
                                            <div :id="'panelsStayOpen-collapseThree-' + ta.id" class="accordion-collapse collapse" aria-labelledby="panelsStayOpen-headingThree">
                                                <div class="accordion-body">
                                                    <div class="subDivQ"  v-for="qual in ta.qualifications" :key="qual.id">
                                                        <div class="col-md-12"><b>{{qual.question.name}}</b>
                                                        </div>
                                                        <div class="col-md-12">{{qual.question.text}}
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div style="display: inline-block" v-for="item in qual.question.variables" :key="item.id">
                                                                    <div class="form-check form-check-inline">
                                                                        <!--<input class="form-check-input" type="checkbox" id="inlineCheckbox1" :value=item.id>-->
                                                                        <label class="form-check-label" for="inlineCheckbox1">{{item.name}}</label>
                                                                    </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                        <a @click="toggleModal(qual.id); useQualStore.GetQualification(qual.question.name.toLowerCase(), ta.id, qual.id)" class="link-primary">Edit</a> |
                                                        <a @click="toggleModal(qual.id)" class="link-danger">Delete</a>
                                                            <CustomModal @close="toggleModal(qual.id)" :modalId=qual.id>
                                                                <div class="card modal-content">
                                                                <h3 class="card-header">Qualifications</h3>
                                                                    <div class="card-body">
                                                                    <QualificationsList :itemType=qual.question.name.toLowerCase() :taId=ta.id :qualificationId=qual.id />
                                                                    </div>
                                                                </div>
                                                            </CustomModal>
                                                        </div>
                                                        <hr/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row g-3 subDiv" v-if="ta.quotas.length > 0">
                                        <h5>Quotas</h5>
                                            <div class="row g-3 subDivQ" v-for="quota in ta.quotas" :key="quota.id">
                                                <div class="col-md-12"><b>Quota details</b>
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="inputEmail4" class="form-label">Name</label>
                                                    <input type="text" class="form-control" id="inputEmail4" v-model="quota.name">
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="inputEmail4" class="form-label">Condition</label>
                                                    <input type="text" class="form-control" id="inputEmail4" v-model="quota.condition">
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="inputEmail4" class="form-label">Limit</label>
                                                    <input type="number" class="form-control" id="inputEmail4" v-model="quota.limit">
                                                </div>
                                                <div class="col-md-6">
                                                    <label for="inputEmail4" class="form-label">Limit Type</label>
                                                    <input type="text" class="form-control" id="inputEmail4" v-model="quota.limitType">
                                                </div>
                                                <div class="col-md-6"/>
                                                <div class="col-md-6">
                                                    <label for="inputEmail4" class="form-label">Enabled</label>
                                                    <div class="form-control" style="border: none"><input type="checkbox" id="inputEmail4" v-model="quota.isActive"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <!--<button class="btn btn-outline-success searchButton me-2 " v-on:click="useProjStore.AddQualificationElement(ta.qualifications)">Add Qualification</button>-->
                                            <button class="btn btn-outline-success searchButton me-2 " v-on:click="useProjStore.AddQuotaElement(ta.quotas)">Add Quota</button>
                                            <button :class="{ hidden: ta.id === 1 }" style="float: right" class="btn btn-outline-success btn-light me-2 " v-on:click="useProjStore.CancelTargetAudience(ta)">Cancel Target Audience</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <button class="btn btn-outline-success btn-light me-2 " v-on:click="useProjStore.AddTargetAudienceElement()">Add another Target Audience</button>

                </div>
            </div>
            
            <div class="col-4">
                <div style="box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px; padding: 15px;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="btn btn-outline-success searchButton me-2" style="width:100%" v-on:click="useProjStore.CalculateCharges()">Calculate Charges</button>
                        </div><div class="breakDiv"></div>
                        <div class="row" v-if="project.projectTargetAudiences" v-for="ta in project.projectTargetAudiences" :key="ta.id">
                            <h5>Estimation of Target audience - {{ta.id}} </h5><div class="breakDiv"></div>
                            <div class="col-md-8">
                                <label for="inputEmail4" class="form-label">CPI</label>
                            </div>
                            <div class="col-md-4">
                                <label for="inputEmail4" class="form-label">{{ta.costPerInterview}} USD</label>
                            </div>
                            <div class="col-md-8">
                                <label for="inputEmail4" class="form-label">CPTG</label>
                            </div>
                            <div class="col-md-4">
                                <label for="inputEmail4" class="form-label">{{ta.cptg}} USD</label>
                            </div><div class="col-md-12"></div>
                            <div class="col-md-8">
                                <label for="inputEmail4" class="form-label"><b>Subtotal</b></label>
                            </div>
                            <div class="col-md-4">
                                <label for="inputEmail4" class="form-label"><b>{{ta.subtotal}} USD</b></label>
                            </div><div class="breakDiv"></div><hr/><div class="breakDiv"></div>
                        </div>
                        
                        <div class="col-md-12">
                            <label for="inputEmail4" class="form-label" style="margin: auto"><h4>Total: {{totalCost}} USD</h4></label>
                        </div>
                        <!--<div class="col-md-6">
                            <label for="inputEmail4" class="form-label"><h4>{{totalCost}} USD</h4></label>
                        </div>--><div class="breakDiv"></div><hr>
                        
                            <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useProjStore.CreateProject(project); this.$router.push('/confirm')">Create Project</button>
                            <button class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;" v-on:click="useProjStore.CreateProject(project); this.$router.push('/confirm')">Save as Draft</button>
                            <button class="btn btn-outline-success btn-light me-2" style="width:100%; margin: 5px 0;" v-on:click="this.$router.push('/')">Cancel</button>

                            <!--<div class="col-md-12"><RouterLink class="btn btn-outline-success searchButton me-2" style="width:100%; margin-bottom: 10px;" to="/confirm">Create Project</RouterLink></div>
                            <div class="col-md-12"><RouterLink class="btn btn-outline-success btn-light me-2" style="width:100%; margin-bottom: 10px;" to="/confirm">Save as Draft</RouterLink></div>
                            <div class="col-md-12"><RouterLink class="btn btn-outline-success btn-light me-2" style="width:100%; margin-bottom: 10px;" to="/confirm">Cancel Project</RouterLink>-->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import CustomModal from '@/components/CustomModal.vue'
import QualificationsList from '@/components/QualificationsList.vue'
import {useProjectStore} from '@/stores/projectStore'
import {useQualificationStore} from '@/stores/qualificationStore'
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'
import {ref} from "vue"

defineProps(['open'])

var useProjStore = useProjectStore()
var useQualStore = useQualificationStore()
const { project, basicSettingDesc, totalCost } = storeToRefs(useProjStore)

const modalActive = ref(false);
const modalId = ref(0);

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
    // modalActive.value = !modalActive.value;
    };



onMounted(() => {
    // console.log('on mounted call');
    useProjStore.$reset()
    useProjStore.AddTargetAudienceElement()
})
</script>
<style>
.hidden {
    display: none;
}
.subDiv {
    margin: 20px auto;
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
    padding-bottom: 20px;
    background-color: #E8E8E8;
}

.subDivQ {
    margin: 10px auto;
    background-color: white;
    /*padding-bottom: 20px;*/
}

.customItem {
    margin-bottom: 10px;
    box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
}

input[type=checkbox] {
    transform: scale(1.5);
}

.breakDiv {
    margin:10px 0;
}

.accordion-item-custom {
    box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
    padding:0;
}

  .modal-content {
    display: flex;
    flex-direction: column;
  }

  .modal-content h1, p {
      margin-bottom: 16px;
    }
  .modal-content h1 {
      font-size: 32px;
    }
  .modal-content p {
      font-size: 18px;
    }

/*.modal1 {
  position: fixed;
  z-index: 999;
  top: 20%;
  left: 50%;
  width: 300px;
  margin-left: -150px;
}*/
    
</style>