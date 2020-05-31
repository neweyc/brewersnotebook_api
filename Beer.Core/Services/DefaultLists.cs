using Beer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Services
{
    public static class DefaultLists
    {
        public static IEnumerable<Yeast> GetDefaultYeast()
        {
            return new Yeast[] {
                new Yeast()
                {
                    Id = Guid.NewGuid(),
                    Name = "British Ale II (1335)",
                    Attenuation=0.75,
                    UnitCost=8.25,
                    Description = "Typical of British and Canadian ale fermentation profile with good flocculating and malty flavor characteristics, crisp finish, clean, fairly dry. Origin: Flocculation: high Attenuation: 73-76% Temperature Range: 63-75° F (17-24° C) Alcohol Tolerance: approximately 10% ABV Styles: American Brown Ale Brown Porter Cream Ale Dry Stout English Barleywine English IPA Extra Special/Strong Bitter (English Pale Ale) Foreign Extra Stout Irish Red Ale Northern English Brown Ale Special/Best/Premium Bitter Standard/Ordinary Bitter"
                },
                new Yeast()
                {
                    Id = Guid.NewGuid(),
                    Name = "Wyeast American Ale II (1272)",
                    Attenuation=0.74,
                    UnitCost=8,
                    Description = "Fruitier and more flocculant than 1056, slightly nutty, soft, clean, slightly tart finish. Apparent attenuation: 72-76%. Flocculation: high. Optimum temp: 60°-72° F"
                },
                new Yeast()
                {
                    Id = Guid.NewGuid(),
                    Name = "Safale S-05 (dry)",
                    Attenuation=0.75,
                    UnitCost=4.00,
                    Description = "A dried American Ale strain with fermentation properties resembling that of Wyeast 1056 (American Ale) or White Labs WLP001 (California Ale). Produces well-balanced beers with low diacetyl and a very clean, crisp palate. Sedimentation is low to medium, and final gravity is medium. Optimum temp: 59°-75° F"
                }
            };
        }

        public static IEnumerable<Fermentable> GetDefaultFermentableCollection()
        {
            return new Fermentable[] {
                new Fermentable()
                {
                    Id = Guid.NewGuid(),
                    Name = "American Pale Malt",
                    Srm=3.4,
                    Ppg = 37,
                    IsExtract = false,
                    UnitCost=1.8,
                    Description = "A fantastic and economical all-purpose base malt. It is made from Harrington barley grown on the western prairies and malted in Minnesota. It is high in enzymes, well modified, clean and smooth. Easily converts with a single step infusion mash."
                },
                new Fermentable()
                {
                    Id = Guid.NewGuid(),
                    Name = "Vienna Malt",
                    IsExtract = false,
                    Srm=3.5,
                    Ppg = 35,
                    UnitCost=1.8,
                    Description = "Full-bodied, golden colored smooth tasting beers. Imparts malty notes to finished beer. Intended for ales and lagers. Flavor: intense malty-sweet, gentle notes of honey and nuts (almond, hazelnut)"
                },
                new Fermentable()
                {
                    Id = Guid.NewGuid(),
                    Name = "Amber DME",
                    IsExtract = true,
                    Srm=10.0,
                    Ppg = 43,
                    UnitCost=1.8,
                    Description = "Dry malt extract"
                }
            };
        }

        public static IEnumerable<Hop> GetDefaultHopsCollection()
        {
            return new Hop[]
            {
                new Hop
                {
                    Id = Guid.NewGuid(),
                    Name="Cascade",
                    AlphaAcidPercentage=6.5,
                    BetaAcidPercentage=6.5,
                    Type = Hop.HopType.Pellet,
                    UnitCost = 2.25,
                    Description = "Cascade is the most popular variety in craft brewing and is known for having a unique floral, spicy and citrus character with balanced bittering potential.  Aroma: Medium intense floral, citrus and grapefruit tones."
                },
                new Hop
                {
                    Id = Guid.NewGuid(),
                    Name="East Kent Golding",
                    AlphaAcidPercentage=6.5,
                    BetaAcidPercentage=2.2,
                    Type = Hop.HopType.Pellet,
                    UnitCost = 2.25,
                    Description = "East Kent Godling is the quintessential English variety. It has been used in kettle and dry hopping and is known for is subtle citrus, floral and herbal characteristics.  Aroma: Specific aroma descriptors include smooth and delicate with floral, lavender, spice, honey, earth, lemon, orange, grapefruit and thyme overtones."
                }
            };
        }
    }
}
