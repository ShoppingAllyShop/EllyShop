import './App.css';
import { Route, Routes } from "react-router-dom";
import Mainlayout from './client/pages/Mainlayout';
import './client/pages/scss/MainLayout.scss'
function App() {
  return (
    <Routes>
      <Route path="/" element={<Mainlayout />}>
        {/* <Route path="/" element={<MainPage />} />
        <Route path="product/:id" element={<ProductDetail />} /> */}
      </Route>
    </Routes>
  );
}

export default App;
