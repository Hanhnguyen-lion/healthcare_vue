import { createI18n } from "vue-i18n";
import en from "./locales/en.json";
import vn from "./locales/vn.json";
import ja from "./locales/ja.json";

const messages = {
  en,
  vn,
  ja
};
const i18n = new createI18n({
  locale: 'en',
  fallbackLocale: 'en',
  legacy: false,
  globalInjection: true,
  messages,
});

export default i18n;