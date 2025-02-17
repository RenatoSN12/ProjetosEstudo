import { useState, useEffect } from "react";
import axios from "axios";

const API_BASE_URL = "http://localhost:5257"

export function useTags() {
  const [tags, setTags] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  useEffect(() => {
    axios.get(`${API_BASE_URL}/api/tag`)
      .then((response) => setTags(response.data))
      .catch(() => setError(true))
      .finally(() => setLoading(false));
  }, []);

  return { tags, loading, error };
}
