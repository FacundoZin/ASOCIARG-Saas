<script setup>
import { useConfigStore } from '@/stores/configStore'

const configStore = useConfigStore()

const tiposAsociacion = ['Cultural', 'Benéfica', 'Jubilados', 'Deportiva', 'Social', 'Otra']
</script>

<template>
  <div class="space-y-6 animate-fade-in">
    <div class="text-center mb-6">
      <h3 class="text-xl font-bold text-white">Datos de la Institución</h3>
      <p class="text-slate-400 text-sm">Información básica para identificar su asociación.</p>
    </div>

    <div class="space-y-4">
      <div>
        <label for="nombre" class="block text-sm font-semibold text-white mb-2">
          Nombre de la asociación
        </label>
        <input
          id="nombre"
          v-model="configStore.config.asociacionNombre"
          type="text"
          placeholder="Ej: Asociación Civil Casa del Jubilado"
          class="w-full px-4 py-3 rounded-xl border border-slate-700 focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all bg-slate-800 text-white"
        />
      </div>

      <div>
        <label class="block text-sm font-semibold text-white mb-3"> Tipo de asociación </label>
        <div class="grid grid-cols-2 sm:grid-cols-3 gap-2">
          <button
            v-for="tipo in tiposAsociacion"
            :key="tipo"
            @click="configStore.config.asociacionTipo = tipo"
            class="px-3 py-2.5 rounded-xl border transition-all text-xs font-bold"
            :class="[
              configStore.config.asociacionTipo === tipo
                ? 'bg-blue-600 border-blue-600 text-white shadow-lg'
                : 'bg-slate-800 border-slate-700 text-slate-300 hover:border-slate-500',
            ]"
          >
            {{ tipo }}
          </button>
        </div>
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
</style>
