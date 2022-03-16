<template>
  <label class="m-checkbox">
    <input ref="cb" type="checkbox" :value="inputValue" v-model="model" />
    <span>
      <slot />
    </span>
  </label>
</template>
<script>
export default {
  props: {
    value:{
        type: Boolean
    },
    inputValue: {
        type: String
    },
    disabled: {
        type: Boolean,
        default: false,
    }
  },
  computed: {
    model: {
      get() {
        return this.value;
      },
      set(value) {
        this.$emit("input", value);
      },
    },
  },
  mounted(){
      if(this.disabled==true){
          this.$refs.cb.disabled = true;
      }
  }
};
</script>
<style scoped src="../../assets/css/base/checkbox.css"></style>
