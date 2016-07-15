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
        Model.Add("stylist", foundStylist);
        Model.Add("clients", foundClients);
        return View["stylist.cshtml", Model];
      };
    }
  }
}
