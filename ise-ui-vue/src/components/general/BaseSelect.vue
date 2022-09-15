<template>
    <label v-if="label">{{ label }}</label>
    <select
      class="field"
      :value="modelValue"
      v-bind="{
        ...$attrs,
        onChange: ($event) => {
          $emit('update:modelValue', $event.target.value);
        },
      }"
    >
      <option value="" disabled>{{ defaultValue }}</option>
      <option
        v-for="option in options"
        :key="option"
        :value="option"
        :selected="option === modelValue"
      >
        {{ option }}
      </option>
    </select>
  </template>
  
  <script>
  export default {
    props: {
      label: {
        type: String,
        default: ""
      },
      defaultValue: {
        type: String,
        default: "--Select--",
        required: false
      },
      modelValue: {
        type: [String, Number],
        default: "10"
      },
      options: {
        type: Array,
        required: true
      },
    },
  };
  </script>

<!-- <BaseSelect
        label="Rows: "
        defaultValue = "--Select--" 
        v-model="currentPageRowCount"
        :options="selectRowCount"
    /> -->