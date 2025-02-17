import { useState, useEffect } from "react";
import axios from "axios";

const API_BASE_URL = "http://localhost:5257"

export function useUsers() {
  const [usuarios, setUsuarios] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    axios.get(`${API_BASE_URL}/api/user`)
      .then((response) => setUsuarios(response.data))
      .catch(() => setError(true))
      .finally(() => setLoading(false));
  }, []);

  return { usuarios, loading, error };
}
