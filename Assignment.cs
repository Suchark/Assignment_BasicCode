class Equipment {
    public int ItemLevel { get; set; }
    public string Type { get; set; }
    public int RefineLevel { get; set; }
}

class RefineCalculator {
    public bool isVIP;

    public RefineCalculator(bool isVIP) {
        this.isVIP = isVIP; 
    }

    public void RefineEquipments(List<Equipment> equipments) {
        for (int i = 0; i < equipments.Count; i++) {
            var equipment = equipments[i];
            if (equipment.ItemLevel < 10) {
                // Check type
                if (equipment.Type == "Weapon") { 
                    RefineWeapon(equipment);
                } else if (equipment.Type == "Armor") {
                    RefineArmor(equipment);
                }
            }
        }
    }

    public void RefineWeapon(Equipment equipment) {
        switch (equipment.ItemLevel) {
            case 1:
                RefineLevel1(equipment, 60, 20);
                break;
            case 2:
                RefineLevel2(equipment, 30, 15);
                break;
            default:
                RefineLevel3AndAbove(equipment, 40, 10);
                break;
        }
    }

    public void RefineArmor(Equipment equipment) {
        if (equipment.RefineLevel < 5) {
            equipment.ItemLevel++;
        } else {
            // Calculate the chance of success based on refine level
            int chance = (10 - equipment.RefineLevel) * 10;
            if (IsSuccess(chance))
                equipment.ItemLevel++;
            else
                equipment.ItemLevel = isVIP ? equipment.ItemLevel - 1 : 0;
        }
    }

    public void RefineLevel1(Equipment equipment, int chance1, int chance2) {
        int chance = equipment.RefineLevel < 7 ? 100 : (equipment.RefineLevel < 9 ? chance1 : chance2);
        if (IsSuccess(chance))
            equipment.ItemLevel++;
        else
            equipment.ItemLevel = isVIP ? equipment.ItemLevel - 1 : 0;
    }

    public void RefineLevel2(Equipment equipment, int chance1, int chance2) {
        int chance = equipment.RefineLevel < 7 ? 100 : (equipment.RefineLevel < 9 ? chance1 : chance2);
        if (IsSuccess(chance))
            equipment.ItemLevel++;
        else
            equipment.ItemLevel = isVIP ? equipment.ItemLevel - 1 : 0;
    }

    public void RefineLevel3AndAbove(Equipment equipment, int chance1, int chance2) {
        int chance = equipment.RefineLevel < 5 ? 100 : (equipment.RefineLevel < 7 ? chance1 : chance2);
        if (IsSuccess(chance))
            equipment.ItemLevel++;
        else
            equipment.ItemLevel = isVIP ? equipment.ItemLevel - 1 : 0;
    }

    public bool IsSuccess(int chance) {
        Random random = new Random();
        int roll = random.Next(0, 100);
        return roll < chance;
    }
}