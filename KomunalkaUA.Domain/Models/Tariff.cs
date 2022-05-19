﻿namespace KomunalkaUA.Domain.Models;

public class Tariff
{
    public int Id { get; set; }
    public double? Watter { get; set; }
    public double? Gas { get; set; }
    public double? Electric { get; set; }
    public int RentRate { get; set; }
    public List<Checkout> Checkouts { get; set; }
}