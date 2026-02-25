<script setup>
import { ref, computed } from 'vue'
import { useRouter } from 'vue-router'
import Step1Institution from '@/components/configs/Step1Institution.vue'
import Step2Payments from '@/components/configs/Step2Payments.vue'
import Step3Modules from '@/components/configs/Step3Modules.vue'
import SuccessConfigModal from '@/components/configs/SuccessConfigModal.vue'
import { useConfigStore } from '@/stores/configStore'

const router = useRouter()
const configStore = useConfigStore()
const currentStep = ref(1)
const showSuccessModal = ref(false)
const steps = [
  { n: 1, label: 'Institución', icon: 'bi bi-building' },
  { n: 2, label: 'Pagos', icon: 'bi bi-credit-card' },
  { n: 3, label: 'Módulos', icon: 'bi bi-grid-fill' },
]

const currentComponent = computed(() => {
  if (currentStep.value === 1) return Step1Institution
  if (currentStep.value === 2) return Step2Payments
  return Step3Modules
})

const progressWidth = computed(() => `${(currentStep.value / steps.length) * 100}%`)

const nextStep = () => {
  if (currentStep.value < 3) {
    currentStep.value++
  } else {
    showSuccessModal.value = true
  }
}

const onConfirmSuccess = () => {
  router.push('/')
}

const prevStep = () => {
  if (currentStep.value > 1) {
    currentStep.value--
  }
}
</script>

<template>
  <div
    class="min-h-screen bg-slate-50 flex items-start justify-center p-4 sm:p-6 pt-16 font-sans relative overflow-hidden"
  >
    <!-- Icono Decorativo Flotante (Posición Media Derecha) -->
    <img
      src="@/assets/iconoasociargsinfondo.png"
      class="fixed top-1/2 -translate-y-1/2 right-[-80px] w-96 opacity-[0.05] pointer-events-none z-0"
      alt="Asociarg Icon"
    />

    <!-- Contenedor ajustado: Más ancho y menos alto -->
    <div
      class="max-w-5xl w-full bg-white rounded-[2.5rem] shadow-2xl overflow-hidden flex flex-col md:flex-row h-[560px] border border-slate-200 relative z-10 transition-all duration-500"
    >
      <!-- Sidebar (IZQUIERDA - FONDO BLANCO) -->
      <div
        class="w-full md:w-64 bg-white p-6 border-r border-slate-100 flex flex-col justify-between"
      >
        <div>
          <!-- Logo -->
          <div class="flex flex-col items-center mb-8">
            <img src="@/assets/LogoAsociarg.png" alt="Asociarg Logo" class="h-10 mb-3" />
            <div class="h-1 w-12 bg-blue-600 rounded-full"></div>
          </div>

          <!-- Stepper Vertical -->
          <div class="space-y-6 relative">
            <div
              class="absolute left-5 top-0 bottom-0 w-0.5 bg-slate-100 -z-1"
              style="margin-bottom: 24px"
            ></div>

            <div
              v-for="step in steps"
              :key="step.n"
              class="flex items-center gap-4 relative z-10 group"
              :class="{ 'opacity-40': currentStep < step.n }"
            >
              <div
                class="w-10 h-10 rounded-2xl flex items-center justify-center transition-all duration-300 font-bold text-base"
                :class="[
                  currentStep === step.n
                    ? 'bg-blue-600 text-white'
                    : currentStep > step.n
                      ? 'bg-emerald-500 text-white'
                      : 'bg-slate-50 text-slate-400 border border-slate-200',
                ]"
              >
                <svg
                  v-if="currentStep > step.n"
                  xmlns="http://www.w3.org/2000/svg"
                  class="h-5 w-5"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                  stroke-width="3"
                >
                  <path stroke-linecap="round" stroke-linejoin="round" d="M5 13l4 4L19 7" />
                </svg>
                <span v-else>{{ step.n }}</span>
              </div>
              <div>
                <p class="text-[9px] uppercase tracking-widest text-slate-400 font-bold mb-0.5">
                  Paso {{ step.n }}
                </p>
                <p class="font-bold text-xs tracking-tight select-none text-slate-700">
                  {{ step.label }}
                </p>
              </div>
            </div>
          </div>
        </div>

        <!-- Tip -->
        <div class="bg-blue-50/50 p-4 rounded-3xl border border-blue-100/50">
          <p class="text-[10px] text-blue-800/60 leading-relaxed font-semibold italic text-center">
            Su configuración se guardará automáticamente.
          </p>
        </div>
      </div>

      <!-- Main Form (DERECHA - FONDO OSCURO) -->
      <div class="flex-grow flex flex-col bg-[#111827] relative">
        <!-- Progress Bar Top -->
        <div class="absolute top-0 left-0 h-1.5 bg-slate-900 w-full z-20">
          <div
            class="h-full bg-blue-500 transition-all duration-700 ease-out shadow-[0_0_15px_rgba(59,130,246,0.6)]"
            :style="{ width: progressWidth }"
          ></div>
        </div>

        <div class="flex-grow p-8 sm:p-10 flex flex-col overflow-hidden">
          <!-- Contenido Dinámico con Scroll Interno -->
          <div class="flex-grow overflow-y-auto custom-scrollbar px-2 mb-4">
            <transition name="step-fade" mode="out-in">
              <component :is="currentComponent" />
            </transition>
          </div>

          <!-- Navigation Buttons -->
          <div class="flex items-center justify-between pt-6 border-t border-slate-800/80">
            <button
              @click="prevStep"
              v-if="currentStep > 1"
              class="flex items-center gap-3 px-6 py-3 text-slate-400 font-bold hover:text-white transition-all group"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                class="h-5 w-5 transition-transform group-hover:-translate-x-1"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M10 19l-7-7m0 0l7-7m-7 7h18"
                />
              </svg>
              Anterior
            </button>
            <div v-else></div>

            <button
              @click="nextStep"
              class="px-10 py-3.5 bg-blue-600 text-white rounded-2xl font-bold flex items-center gap-3 hover:bg-blue-700 active:scale-95 transition-all"
            >
              {{ currentStep === 3 ? 'Guardar Configuración' : 'Siguiente Paso' }}
              <svg
                v-if="currentStep < 3"
                xmlns="http://www.w3.org/2000/svg"
                class="h-5 w-5"
                fill="none"
                viewBox="0 0 24 24"
                stroke="currentColor"
              >
                <path
                  stroke-linecap="round"
                  stroke-linejoin="round"
                  stroke-width="2"
                  d="M14 5l7 7m0 0l-7 7m7-7H3"
                />
              </svg>
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- Success Modal -->
    <SuccessConfigModal
      :is-open="showSuccessModal"
      @confirm="onConfirmSuccess"
      @close="showSuccessModal = false"
    />
  </div>
</template>

<style scoped>
.step-fade-enter-active,
.step-fade-leave-active {
  transition: all 0.3s ease;
}

.step-fade-enter-from {
  opacity: 0;
  transform: translateY(10px);
}
.step-fade-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}

.glow-blue {
  display: none;
}

.custom-scrollbar::-webkit-scrollbar {
  width: 6px;
}

.custom-scrollbar::-webkit-scrollbar-track {
  background: transparent;
}

.custom-scrollbar::-webkit-scrollbar-thumb {
  background: rgba(59, 130, 246, 0.15);
  border-radius: 20px;
  border: 2px solid transparent;
  background-clip: content-box;
  transition: all 0.3s ease;
}

.custom-scrollbar::-webkit-scrollbar-thumb:hover {
  background-color: rgba(59, 130, 246, 0.4);
}

/* Firefox Support */
.custom-scrollbar {
  scrollbar-width: thin;
  scrollbar-color: rgba(59, 130, 246, 0.2) transparent;
}
</style>
