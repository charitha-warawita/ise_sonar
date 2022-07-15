<template>
  <div class="container">
    <div class="row">
      <div class="col-md-6">
        <label for="input" class="form-label">Name</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.name" />
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Field Target(in percentage)</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.fieldTarget" />
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Status</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.status" />
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Completes</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.completes" />
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Prescreence</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.prescreence" />
      </div>
      <div class="col-md-6">
        <label for="sel1" class="form-label">Select Condition</label>
        <select
          class="form-select"
          id="sel1"
          name="sellist1"
          v-model="key"
          @change="useQuotaDataStore.selectQuotaCondition($event)">
          <option v-bind:value=condition selected="[condition === 'None' ? 'selected' : '']"  v-for="condition in useQuotaDataStore.currentQuota.conditions" :key="condition">{{condition}}</option>
        </select>
      </div>
    </div>
  </div>
  <div v-if="useQuotaDataStore.showSubPopup">
  <div class="container" v-if="useQuotaDataStore.showQuotaCondition === 'none'">
  </div>
  <div class="container" v-if="useQuotaDataStore.showQuotaCondition === 'country'">
    <div class="row">
      <div class="col-md-12">
        <div class="card" style="margin-top: 5%">
          <div div class="card-body">
            <div class="col-md-12"><h5>Select countries</h5></div>
            <div
              style="display: inline-block"
              v-for="item in useQuotaDataStore.quotaCountries"
              :key="item.id"
            >
              <div class="form-check form-check-inline">
                <input
                  class="form-check-input"
                  type="checkbox"
                  v-model="item.selected"
                  :checked="item.selected"
                  id="inlineCheckbox1"
                  :value="item.id"
                />
                <label class="form-check-label" for="inlineCheckbox1">{{
                  item.name
                }}</label>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="container" v-if="useQuotaDataStore.showQuotaCondition === 'age'">
    <div class="row">
      <div class="col-md-12">
        <div class="card" style="margin-top: 5%">
          <div div class="card-body">
            <div class="col-md-12">
              <h5>Current Age range is set to {{ currAgeRange }}. Enter new age range</h5>
            </div>
            <div class="row">
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Min Age</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputEmail4"
                  v-model="useQuotaDataStore.currentQuota.quotaMinAge"
                />
              </div>
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Max Age</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputEmail4"
                  v-model="useQuotaDataStore.currentQuota.quotaMaxAge"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="container" v-if="useQuotaDataStore.showQuotaCondition === 'gender'">
    <div class="row">
      <div class="col-md-12">
        <div class="card" style="margin-top: 5%">
          <div class="row">
            <div div class="card-body">
              <div class="col-md-12"><h5>Select genders</h5></div>
              <div class="col-md-12">
                <div
                  style="display: inline-block"
                  v-for="item in useQuotaDataStore.quotaGenders"
                  :key="item.id"
                >
                  <div class="form-check form-check-inline">
                    <input
                      class="form-check-input"
                      type="checkbox"
                      v-model="item.selected"
                      :checked="item.selected"
                      id="inlineCheckbox1"
                      :value="item.id"
                    />
                    <label class="form-check-label" for="inlineCheckbox1">{{
                      item.name
                    }}</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
  <div class="col-md-12">
    <button
      class="btn btn-outline-success searchButton me-2"
      style="width: 100%; margin: 5px 0"
      v-on:click="
        useQuotaDataStore.SaveQuota(useQuotaDataStore.showQuotaCondition, taId, quotaid)
      "
    >
      Save quota
    </button>
  </div>
</template>
<script setup>
import { useQuotaStore } from "@/stores/quotaStore";
import { storeToRefs } from "pinia";
import { onMounted } from "vue";
const props = defineProps(["itemType", "taId", "quotaid"]);

var useQuotaDataStore = useQuotaStore();

const {
  name,
  fieldTarget,
  status,
  completes,
  prescreence,
  quotaMinAge,
  quotaMaxAge,
} = storeToRefs(useQuotaDataStore);
</script>
