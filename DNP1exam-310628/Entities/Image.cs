using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Entities; 

public class Image {
    // See model creation for Primary key.
    // URI and title are selected to be the composite Pk


    [Required, MaxLength(25)]
    public string Title { get; set; }
    
    public string? Description { get; set; }

    [Required]
    public string URL { get; set; }

    [Required]
    public string Topic { get; set; }

    // For efc endpoint
    public Album? Album { get; set; }




}