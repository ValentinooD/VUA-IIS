package valentinood.iis;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "Word")
public class Word {
    private String String;
    private String Meaning;
    private int Invented;

    @XmlElement(name = "String")
    public String getString() {
        return String;
    }

    public void setString(String string) {
        this.String = string;
    }

    @XmlElement(name = "Meaning")
    public String getMeaning() {
        return Meaning;
    }

    public void setMeaning(String meaning) {
        this.Meaning = meaning;
    }

    @XmlElement(name = "Invented")
    public int getInvented() {
        return Invented;
    }

    public void setInvented(int invented) {
        this.Invented = invented;
    }

    @Override
    public String toString() {
        return String + ": " + Meaning;
    }
}
