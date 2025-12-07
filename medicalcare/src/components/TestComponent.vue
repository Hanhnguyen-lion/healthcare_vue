<template>
	<div>
		<div class="container">


			<button class="btn btn-success" @click="openModal">Show Modal</button>
			<transition name="modal">
				<div class="modal-mask" v-if="showModal">
					<modal :input_data="this.item" @close="closeModal"/>
				</div>
			</transition>
		</div>
	</div>
</template>

<script>
import modal from "@/components/ModalComponent";

export default {
  name: "TestComponent",
	components: {modal},
	data:function (){
		return{
      item:{
        id: 1,
        name: "test"
      },
			showModal: false
		}
	},
	methods:{
    openModal(){
      this.showModal = true;
    },
		closeModal(data){
      console.log("data: ", data.title, data.saveData);
      this.showModal = false;
		}
	}
}
</script>