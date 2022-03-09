import java.io.Serializable;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

public class VulnClass implements VulnInterface, Serializable {
    public void vulnMethod(){
        Class clazz = Object.class;
        Object o = new Object();
        try {
            Method m = clazz.getMethod("toString");
            m.invoke(o, null);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
