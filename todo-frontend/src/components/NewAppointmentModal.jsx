import React, { useState } from "react";
import {
  Modal,
  Box,
  TextField,
  Button,
  Typography,
  MenuItem,
  OutlinedInput,
  InputLabel,
  FormControl,
  ListItemText,
  Select,
  Checkbox,
  Snackbar,
  Alert,
  ThemeProvider,
  createTheme,
} from "@mui/material";
import dayjs from "dayjs";
import { useUsers } from "../hooks/useUsers";
import { useTags } from "../hooks/useTags";
import { useSaveAppointment } from "../hooks/useSaveAppointment";

// Configuração do tema escuro
const darkTheme = createTheme({
  palette: {
    mode: "dark", // Modo escuro
    background: {
      default: "#1a1a1a", // Fundo escuro
      paper: "#1a1a1a", // Fundo de componentes como Paper, Modal, etc.
    },
    text: {
      primary: "#cccccc", // Texto cinza claro
      secondary: "#cccccc", // Texto cinza claro para secundário
    },
    action: {
      active: "#cccccc", // Ícones e ações ativos em cinza claro
    },
  },
  components: {
    // Estilo para TextField
    MuiTextField: {
      styleOverrides: {
        root: {
          "& .MuiOutlinedInput-root": {
            "& fieldset": {
              borderColor: "#cccccc", // Borda cinza claro
            },
            "&:hover fieldset": {
              borderColor: "#cccccc", // Borda cinza claro ao passar o mouse
            },
          },
          "& .MuiInputLabel-root": {
            color: "#cccccc", // Label cinza claro
          },
          "& .MuiInputBase-input": {
            color: "#cccccc", // Texto do input cinza claro
          },
        },
      },
    },
    // Estilo para Select (campo "Tags")
    MuiSelect: {
      styleOverrides: {
        root: {
          "& .MuiOutlinedInput-root": {
            "& fieldset": {
              borderColor: "#cccccc", // Borda cinza claro
            },
            "&:hover fieldset": {
              borderColor: "#cccccc", // Borda cinza claro ao passar o mouse
            },
          },
          "& .MuiInputLabel-root": {
            color: "#cccccc", // Label cinza claro
          },
          "& .MuiInputBase-input": {
            color: "#cccccc", // Texto do input cinza claro
          },
        },
        icon: {
          color: "#cccccc", // Ícone do Select cinza claro
        },
      },
    },
    // Estilo para Menu (itens expandidos do Select)
    MuiMenu: {
      styleOverrides: {
        paper: {
          backgroundColor: "#1a1a1a", // Fundo escuro do menu
          color: "#cccccc", // Texto cinza claro no menu
        },
      },
    },
    // Estilo para MenuItem (itens do menu)
    MuiMenuItem: {
      styleOverrides: {
        root: {
          color: "#cccccc", // Texto cinza claro
          "&:hover": {
            backgroundColor: "rgba(204, 204, 204, 0.08)", // Fundo ao passar o mouse
          },
        },
      },
    },
    // Estilo para Checkbox
    MuiCheckbox: {
      styleOverrides: {
        root: {
          color: "#cccccc", // Cor do Checkbox cinza claro
          "&.Mui-checked": {
            color: "#cccccc", // Cor do Checkbox quando marcado
          },
        },
      },
    },
    // Estilo para Button
    MuiButton: {
      styleOverrides: {
        outlined: {
          color: "#cccccc", // Texto do botão outlined cinza claro
          borderColor: "#cccccc", // Borda do botão outlined cinza claro
          "&:hover": {
            borderColor: "#cccccc", // Borda ao passar o mouse
            backgroundColor: "rgba(204, 204, 204, 0.08)", // Fundo ao passar o mouse
          },
        },
      },
    },
  },
});

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
      backgroundColor: "#1a1a1a", // Fundo escuro do menu de seleção
      color: "#cccccc", // Texto cinza claro no menu de seleção
    },
  },
};

export default function NewAppointmentModal({ open, handleClose }) {
  const [descricao, setDescricao] = useState("");
  const [dataPrevista, setDataPrevista] = useState("");
  const [responsavel, setResponsavel] = useState("");
  const [selectedTags, setSelectedTags] = useState([]);
  const [snackbarOpen, setSnackbarOpen] = useState(false);
  const [snackbarMessage, setSnackbarMessage] = useState("");
  const [snackbarSeverity, setSnackbarSeverity] = useState("success");
  const dataCadastro = dayjs().format("DD/MM/YYYY HH:mm");
  const { usuarios, loading: loadingUsers, error: errorUsers } = useUsers();
  const { tags, loading: loadingTags, error: errorTags } = useTags();
  const { saveAppointment, loading: saving, error: saveError } = useSaveAppointment();

  const handleSave = async () => {
    const appointmentData = {
      descricao,
      dataPrevista,
      dataCadastro,
      responsavel,
      tags: selectedTags,
    };

    try {
      await saveAppointment(appointmentData);
      setSnackbarMessage("Compromisso salvo com sucesso!");
      setSnackbarSeverity("success");
      setSnackbarOpen(true);
      handleClose();
    } catch (error) {
      setSnackbarMessage("Erro ao salvar compromisso. Tente novamente.");
      setSnackbarSeverity("error");
      setSnackbarOpen(true);
    }
  };

  const handleTagChange = (event) => {
    const {
      target: { value },
    } = event;
    setSelectedTags(
      typeof value === 'string' ? value.split(',') : value,
    );
  };

  const handleSnackbarClose = () => {
    setSnackbarOpen(false);
  };

  return (
    <ThemeProvider theme={darkTheme}>
      <Modal open={open} onClose={handleClose}>
        <Box
          sx={{
            position: "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            width: 400,
            bgcolor: "background.paper", // Usa a cor do tema
            boxShadow: 24,
            p: 4,
            borderRadius: 2,
          }}
        >
          <Typography variant="h6" mb={3} sx={{ fontWeight: "bold", textAlign: "center" }}>
            Novo Compromisso
          </Typography>

          {/* Campo de Descrição */}
          <TextField
            fullWidth
            required
            label="Descrição"
            variant="outlined"
            margin="normal"
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
            sx={{ mb: 2 }} // Margem inferior
          />

          {/* Campo de Data Prevista */}
          <TextField
            fullWidth
            required
            label="Data Prevista de Conclusão"
            type="date"
            variant="outlined"
            margin="normal"
            InputLabelProps={{ shrink: true }}
            value={dataPrevista}
            onChange={(e) => setDataPrevista(e.target.value)}
            sx={{ mb: 2 }} // Margem inferior
          />

          {/* Campo de Responsável */}
          <TextField
            label="Responsável"
            select
            required
            fullWidth
            margin="normal"
            value={responsavel}
            onChange={(e) => setResponsavel(e.target.value)}
            sx={{ mb: 2 }} // Margem inferior
          >
            {loadingUsers ? (
              <MenuItem disabled>Carregando...</MenuItem>
            ) : errorUsers ? (
              <MenuItem disabled>Erro ao carregar usuários</MenuItem>
            ) : (
              usuarios.map((user) => (
                <MenuItem key={user.id} value={user.name}>
                  {user.name}
                </MenuItem>
              ))
            )}
          </TextField>

          {/* Campo de Tags */}
          <FormControl fullWidth margin="normal">
            <InputLabel id="tags-checkbox-label">Tags</InputLabel>
            <Select
              labelId="tags-checkbox-label"
              id="tags-checkbox"
              multiple
              value={selectedTags}
              onChange={handleTagChange}
              input={<OutlinedInput label="Tags" />}
              renderValue={(selected) => selected.join(', ')}
              MenuProps={MenuProps}
              sx={{ mb: 2 }} // Margem inferior
            >
              {loadingTags ? (
                <MenuItem disabled>Carregando tags...</MenuItem>
              ) : errorTags ? (
                <MenuItem disabled>Erro ao carregar tags</MenuItem>
              ) : (
                tags.map((tag) => (
                  <MenuItem key={tag.id} value={tag.name}>
                    <Checkbox checked={selectedTags.includes(tag.name)} />
                    <ListItemText primary={tag.name} />
                  </MenuItem>
                ))
              )}
            </Select>
          </FormControl>

          {/* Botões de Salvar e Cancelar */}
          <Box sx={{ display: "flex", justifyContent: "space-between", mt: 3 }}>
            <Button
              variant="contained"
              color="primary"
              onClick={handleSave}
              disabled={saving}
              sx={{ flex: 1, mr: 1, backgroundColor: "#cccccc", color: "#1a1a1a", "&:hover": { backgroundColor: "#b3b3b3" } }}
            >
              {saving ? "Salvando..." : "Salvar"}
            </Button>
            <Button
              variant="outlined"
              color="secondary"
              onClick={handleClose}
              sx={{ flex: 1, ml: 1 }}
            >
              Cancelar
            </Button>
          </Box>
        </Box>
      </Modal>

      <Snackbar
        open={snackbarOpen}
        autoHideDuration={6000}
        onClose={handleSnackbarClose}
      >
        <Alert onClose={handleSnackbarClose} severity={snackbarSeverity} sx={{ width: '100%' }}>
          {snackbarMessage}
        </Alert>
      </Snackbar>
    </ThemeProvider>
  );
}