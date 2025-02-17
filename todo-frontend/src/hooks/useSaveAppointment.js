import { useState } from "react";
import axios from "axios";

const API_BASE_URL = "http://localhost:5257";

export function useSaveAppointment() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const saveAppointment = async (appointmentData) => {
    setLoading(true);
    setError(null);

    try {
      const response = await axios.post(`${API_BASE_URL}/api/appointment`, appointmentData);
      setLoading(false);
      return response.data; // Retorna os dados da resposta (opcional)
    } catch (err) {
      setError(err.message || "Erro ao salvar compromisso.");
      setLoading(false);
      throw err; // Lança o erro para ser tratado no componente
    }
  };

  return { saveAppointment, loading, error };
}