using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Appointment> allAppointments = Appointment.GetAll();
        return View["index.cshtml", allAppointments];
      };

      Get["/stylist/new"] = _ => {
        return View ["stylist_form.cshtml"];
      };
      Post["/stylist/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist_name"]);
        newStylist.Save();
        return View["success.cshtml", newStylist];
      };

      Get["/stylist/all"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View["stylists.cshtml", allStylists];
      };

      Get["stylist/{id}"] = parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundStylist = Stylist.Find(parameters.id);
        var foundClients = foundStylist.GetClients();
        var foundApppointments = foundStylist.GetAppointments();
        Model.Add("stylist", foundStylist);
        Model.Add("clients", foundClients);
        Model.Add("appointments", foundApppointments);
        return View["stylist.cshtml", Model];
      };



      Get["/client/new"] = _ => {
        List<Stylist> allStylists = Stylist.GetAll();
        return View ["client_form.cshtml", allStylists];
      };
      Post["/client/new"] = _ => {
        Client newClient = new Client(Request.Form["client_name"], Request.Form["stylist_id"]);
        newClient.Save();
        return View["success.cshtml", newClient];
      };
      Get["/client/all"] = _ => {
        List<Client> allClients = Client.GetAll();
        return View["clients.cshtml", allClients];
      };
      Get["client/{id}"] = parameters => {
        Dictionary<string, object> Model = new Dictionary<string, object>();
        var foundClient = Client.Find(parameters.id);
        var foundStylist = Stylist.Find(foundClient.GetStylistId());

        var foundAppointments = foundClient.GetAppointments();
        Model.Add("stylist", foundStylist);
        Model.Add("client", foundClient);
        Model.Add("appointments", foundAppointments);
        return View["client.cshtml", Model];
      };

    }
  }
}
