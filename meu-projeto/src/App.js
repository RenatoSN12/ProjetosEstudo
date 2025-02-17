import './App.css';
import { BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import Home from './pages/Home';
import Contato from './pages/Contato';
import Empresa from './pages/Empresa';
import Navbar from './components/layout/Navbar';
import Footer from './components/layout/Footer';

function App() {

  return (
    <Router>
      <Navbar/>
      <Routes>
        <Route path='/' element={<Home/>}/>
        <Route path='/empresa' element={<Empresa/>}/>
        <Route path='/contato' element={<Contato/>}/>
      </Routes>
      <Footer/>
    </Router>
  );
}

export default App;
