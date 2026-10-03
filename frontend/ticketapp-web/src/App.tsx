import {Routes, Route} from "react-router-dom";
import { TicketList } from "./components/TicketList";
import {TicketDetails} from "./components/TicketDetails";

function App() {
  return (
    <Routes>
      <Route path ="/" element ={<TicketList/>}/>
      <Route path="/tickets/:id" element = {<TicketDetails/>}/>
    </Routes>
  )
}

export default App;