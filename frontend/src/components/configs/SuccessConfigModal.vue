<script setup>
defineProps({
  isOpen: Boolean,
})

const emit = defineEmits(['close', 'confirm'])

const handleConfirm = () => {
  emit('confirm')
  emit('close')
}
</script>

<template>
  <Transition name="modal">
    <div v-if="isOpen" class="fixed inset-0 z-[100] flex items-center justify-center p-4">
      <!-- Backdrop -->
      <div
        class="fixed inset-0 bg-slate-900/40 backdrop-blur-sm transition-opacity"
        @click="handleConfirm"
      ></div>

      <!-- Modal Content -->
      <div
        class="relative w-full max-w-[280px] transform overflow-hidden rounded-2xl bg-white p-5 text-center shadow-xl transition-all border border-slate-100"
      >
        <!-- Icon -->
        <div class="mb-4 flex justify-center">
          <div
            class="w-12 h-12 bg-slate-50 rounded-full flex items-center justify-center border border-slate-100 shadow-sm"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-6 w-6 text-emerald-600 animate-check-draw"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2.5"
                d="M5 13l4 4L19 7"
              />
            </svg>
          </div>
        </div>

        <!-- Text Content -->
        <h3 class="text-lg font-semibold text-slate-900 mb-1">Configuración Lista</h3>
        <p class="text-xs text-slate-500 mb-6 leading-relaxed px-2">
          Los cambios se guardaron con éxito. Bienvenido a
          <span class="font-semibold text-slate-700">ASOCIARG</span>.
        </p>

        <!-- Action Button -->
        <button
          @click="handleConfirm"
          class="w-full py-2.5 bg-slate-900 text-white rounded-lg font-medium text-sm hover:bg-slate-800 transition-colors duration-200"
        >
          Ir a la pantalla principal
        </button>
      </div>
    </div>
  </Transition>
</template>

<style scoped>
.modal-enter-active,
.modal-leave-active {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}

.modal-enter-from {
  opacity: 0;
  transform: scale(0.95) translateY(10px);
}

.modal-leave-to {
  opacity: 0;
  transform: scale(0.98);
}

@keyframes check-draw {
  from {
    stroke-dasharray: 50;
    stroke-dashoffset: 50;
  }
  to {
    stroke-dasharray: 50;
    stroke-dashoffset: 0;
  }
}

.animate-check-draw {
  animation: check-draw 0.6s ease-out forwards;
}
</style>
