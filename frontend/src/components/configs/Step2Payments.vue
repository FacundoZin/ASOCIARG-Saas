<script setup>
import { useConfigStore } from '@/stores/configStore'

const configStore = useConfigStore()

const periodos = [
  { label: 'Mensual', value: 'mensual' },
  { label: 'Bimestral', value: 'bimestral' },
  { label: 'Trimestral', value: 'trimestral' },
  { label: 'Semestral', value: 'semestral' },
  { label: 'Anual', value: 'anual' },
]
</script>

<template>
  <div class="space-y-6 animate-fade-in">
    <div class="text-center mb-6">
      <h3 class="text-xl font-bold text-white">Costo y Periodicidad</h3>
      <p class="text-slate-400 text-sm">Configure el cobro de cuotas.</p>
    </div>

    <div class="space-y-5">
      <!-- Periodo de Pago -->
      <div>
        <label class="block text-sm font-semibold text-white mb-4"> Periodo de pago </label>
        <div class="grid grid-cols-2 sm:grid-cols-3 gap-2">
          <button
            v-for="periodo in periodos"
            :key="periodo.value"
            @click="configStore.config.pagoPeriodo = periodo.value"
            class="px-3 py-2.5 rounded-xl border transition-all text-xs font-bold flex items-center gap-2"
            :class="[
              configStore.config.pagoPeriodo === periodo.value
                ? 'bg-blue-600 border-blue-600 text-white shadow-lg'
                : 'bg-slate-800 border-slate-700 text-slate-300 hover:border-slate-500',
            ]"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-4 w-4"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z"
              />
            </svg>
            {{ periodo.label }}
          </button>
        </div>
      </div>

      <!-- Valor de la Cuota -->
      <div>
        <label for="cuota" class="block text-sm font-semibold text-white mb-2">
          Valor de la cuota ($)
        </label>
        <div class="relative max-w-xs mx-auto md:mx-0">
          <span class="absolute left-4 top-1/2 -translate-y-1/2 text-blue-500 font-bold text-lg"
            >$</span
          >
          <input
            id="cuota"
            v-model.number="configStore.config.cuotaValor"
            type="number"
            placeholder="0.00"
            class="no-spinner w-full pl-10 pr-4 py-3 rounded-xl border border-slate-700 bg-slate-800 focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all text-white"
          />
        </div>
        <p class="mt-2 text-[10px] text-slate-500 italic">
          * Este valor se aplicará por defecto a los nuevos socios.
        </p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.animate-fade-in {
  animation: fadeIn 0.4s ease-out;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

/* Hide Spinners as requested in previous steps but reverted by user edit */
.no-spinner::-webkit-outer-spin-button,
.no-spinner::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}
.no-spinner {
  -moz-appearance: textfield;
  appearance: textfield;
}
</style>
