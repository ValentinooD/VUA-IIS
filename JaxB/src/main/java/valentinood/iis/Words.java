package valentinood.iis;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "Words")
public class Words {
    private List<Word> Words;

    @XmlElement(name = "Word")
    public List<Word> getWords() {
        return Words;
    }

    public void setWords(List<Word> words) {
        this.Words = words;
    }
}
