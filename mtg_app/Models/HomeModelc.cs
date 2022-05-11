using System;
using System.ComponentModel.DataAnnotations;
using mtg_lib.Library.Services;

namespace mtg_app.Models

{
    public class HomeModel {
    
        public bool ShowContent {get;set;}
        public List<String> Session {get;set;}
        
    }
}