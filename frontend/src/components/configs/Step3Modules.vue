<script setup>
import { useConfigStore } from '@/stores/configStore'

const configStore = useConfigStore()

const modulosList = [
  {
    id: 'socios',
    nombre: 'Módulo de Socios',
    desc: 'Gestión completa de padrón y datos',
    icon: 'M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0z',
  },
  {
    id: 'cobradores',
    nombre: 'Cobradores',
    desc: 'Asignación de rutas y cobranza móvil',
    icon: 'M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z',
  },
  {
    id: 'cuotas',
    nombre: 'Cuotas',
    desc: 'Generación y control de pagos mensuales',
    icon: 'M12 8c-1.657 0-3 .895-3 2s1.343 2 3 2 3 .895 3 2-1.343 2-3 2m0-8c1.11 0 2.08.402 2.599 1M12 8V7m0 1v8m0 0v1m0-1c-1.11 0-2.08-.402-2.599-1M21 12a9 0 11-18 0 9 9 0 0118 0z',
  },
  {
    id: 'salones',
    nombre: 'Reserva de Salones',
    desc: 'Calendario y gestión de espacios',
    icon: 'M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z',
  },
  {
    id: 'articulos',
    nombre: 'Alquiler de Artículos',
    desc: 'Control de inventario de ortopedia',
    icon: 'M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4',
  },
  {
    id: 'viajes',
    nombre: 'Gestión de Viajes',
    desc: 'Organización de turismo y excursiones',
    icon: 'M9 20l-5.447-2.724A1 1 0 013 16.382V5.618a1 1 0 011.447-.894L9 7m0 13l6-3m-6 3V7m6 10l4.553 2.276A1 1 0 0021 18.382V7.618a1 1 0 00-.553-.894L15 4m0 13V4m0 0L9 7',
  },
  {
    id: 'estadisticas',
    nombre: 'Estadísticas y Balances',
    desc: 'Informes detallados y gráficos',
    icon: 'M9 19v-6a2 2 0 00-2-2H5a2 2 0 00-2 2v6a2 2 0 002 2h2a2 2 0 002-2zm0 0V9a2 2 0 012-2h2a2 2 0 012 2v10m-6 0a2 2 0 002 2h2a2 2 0 002-2m0 0V5a2 2 0 012-2h2a2 2 0 012 2v14a2 2 0 01-2 2h-2a2 2 0 01-2-2z',
  },
]

const toggleModulo = (id) => {
  configStore.config.modulos[id] = !configStore.config.modulos[id]
}
</script>

<template>
  <div class="space-y-6 animate-fade-in">
    <div class="text-center mb-6">
      <h3 class="text-2xl font-bold text-white">Seleccione los Módulos</h3>
      <p class="text-slate-400">Marque las herramientas que desea utilizar.</p>
    </div>

    <div class="grid grid-cols-1 md:grid-cols-2 gap-4 pb-4">
      <div
        v-for="modulo in modulosList"
        :key="modulo.id"
        @click="toggleModulo(modulo.id)"
        class="relative p-5 rounded-2xl border transition-all duration-200 cursor-pointer group active:scale-[0.98]"
        :class="[
          configStore.config.modulos[modulo.id]
            ? 'border-blue-500/50 bg-blue-500/5'
            : 'border-slate-800 bg-[#1f2937]/30 hover:border-slate-700 hover:bg-[#1f2937]/50',
        ]"
      >
        <div class="flex items-center gap-4">
          <div
            class="w-11 h-11 rounded-xl flex items-center justify-center transition-all duration-200"
            :class="[
              configStore.config.modulos[modulo.id]
                ? 'bg-blue-600/90 text-white'
                : 'bg-slate-800 text-slate-500 group-hover:text-slate-400',
            ]"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              class="h-5.5 w-5.5"
              fill="none"
              viewBox="0 0 24 24"
              stroke="currentColor"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="1.8"
                :d="modulo.icon"
              />
            </svg>
          </div>
          <div class="flex-grow">
            <h4
              class="font-bold transition-all text-sm"
              :class="configStore.config.modulos[modulo.id] ? 'text-blue-100' : 'text-slate-300'"
            >
              {{ modulo.nombre }}
            </h4>
            <p class="text-[11px] text-slate-500 mt-0.5 leading-tight font-normal">
              {{ modulo.desc }}
            </p>
          </div>
          <div>
            <div
              class="w-5 h-5 rounded border flex items-center justify-center transition-all duration-200"
              :class="[
                configStore.config.modulos[modulo.id]
                  ? 'border-blue-500/50 bg-blue-600 text-white'
                  : 'border-slate-700 group-hover:border-slate-600',
              ]"
            >
              <svg
                v-if="configStore.config.modulos[modulo.id]"
                xmlns="http://www.w3.org/2000/svg"
                class="h-3.5 w-3.5"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="4"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <polyline points="20 6 9 17 4 12"></polyline>
              </svg>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.animate-fade-in {
  animation: fadeIn 0.4s ease-out;
}

.glow-blue-sm {
  display: none;
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
