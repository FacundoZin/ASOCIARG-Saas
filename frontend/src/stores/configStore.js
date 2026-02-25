import { defineStore } from 'pinia'
import { ref, reactive, computed } from 'vue'

export const useConfigStore = defineStore('config', () => {
  const config = reactive({
    asociacionNombre: '',
    asociacionTipo: '',
    pagoPeriodo: 'mensual',
    cuotaValor: 0,
    modulos: {
      socios: false,
      cobradores: false,
      cuotas: false,
      salones: false,
      articulos: false,
      viajes: false,
      estadisticas: false
    }
  })

  const updateConfig = (newData) => {
    Object.assign(config, newData)
  }

  const asociacionNombre = computed(() => {
    return config.asociacionNombre || 'El Nombre De Tu Asociación'
  })

  return {
    config,
    asociacionNombre,
    updateConfig
  }
})
