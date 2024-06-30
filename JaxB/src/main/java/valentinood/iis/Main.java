package valentinood.iis;

import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.JAXBException;
import jakarta.xml.bind.Unmarshaller;
import org.xml.sax.SAXException;

import javax.xml.XMLConstants;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import java.io.File;
import java.util.Arrays;
import java.util.Optional;

public class Main {
    private static final String PATH_SCHEMA = "../API/Schemas/Schema.xsd";
    private static final String PATH_FILES = "../API/files/";

    public static void main(String[] args) {
        File filesDir = new File(PATH_FILES);
        File[] filesArr = filesDir.listFiles();

        if (filesArr == null) {
            System.err.println("There are no files in " + filesDir.getAbsolutePath());
            return;
        }

        Optional<File> target = Arrays.stream(filesArr)
                .sorted((a, b) -> Math.toIntExact(b.lastModified() - a.lastModified())).findFirst();

        if (target.isEmpty()) {
            System.err.println("Could not find last created file.");
            return;
        }

        try {
            JAXBContext ctx = JAXBContext.newInstance(Words.class);

            Unmarshaller unmarshaller = ctx.createUnmarshaller();

            SchemaFactory schemaFactory = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema schema = schemaFactory.newSchema(new File(PATH_SCHEMA));
            unmarshaller.setSchema(schema);

            Words words = (Words) unmarshaller.unmarshal(target.get());
            System.out.println("Unmarshalled. List of words:");
            for (Word word : words.getWords()) {
                System.out.println(word.toString());
            }
            System.out.println("Yes. The XSD matches the file generated");
        } catch (JAXBException ex) {
            System.err.println("No. The XSD does not match the file generated");
            ex.printStackTrace();
        } catch (SAXException ex) {
            System.err.println("Exception occurred while parsing schema.");
            ex.printStackTrace();
        }
    }
}
